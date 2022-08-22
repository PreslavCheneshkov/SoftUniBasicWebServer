using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.HTTP
{
    public class HttpServer : IHttpServer
    {
        IList<Route> routeTable;
        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                // purposely not awaiting
                ProcessClientAsync(tcpClient);
            }
        }
        public async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    int position = 0;
                    byte[] buffer = new byte[HttpConstants.BufferSize];
                    // todo: research faster data strucutre for array of  bytes
                    List<byte> data = new List<byte>();
                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, 0, buffer.Length);
                        position += count;

                        if (count < buffer.Length)
                        {
                            var bufferWithData = new byte[count];
                            Array.Copy(buffer, bufferWithData, count);
                            data.AddRange(bufferWithData);
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }
                    //byte[] => string (text) -- encoding

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine($"{request.Method} {request.Path} => {request.Headers.Count} headers");

                    HttpResponse response;
                    var route = this.routeTable.FirstOrDefault(x => string.Compare(x.Path, request.Path, true) == 0
                                                                    && x.Method == request.Method);
                    if (route != null)
                    {
                        response = route.Action(request);
                    }
                    else
                    {
                        // not found
                        response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                    }

                    response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                    { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
                    response.Headers.Add(new Header("Server:", "SoftUniBasicWebServer 1.0"));
                    var reponseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                    await stream.WriteAsync(reponseHeaderBytes, 0 , reponseHeaderBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            tcpClient.Close();
        }
    }
}
