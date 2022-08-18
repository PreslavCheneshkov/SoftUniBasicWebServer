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
        IDictionary<string, Func<HttpRequest, HttpResponse>> routeTable 
                                     = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (routeTable.ContainsKey(path))
            {
                routeTable[path] = action;
            }
            else
            {
                routeTable.Add(path, action);
            }
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



                    //todo extract info as requestAsString
                    var responseHtml = "<h1>Hello, Welcome!</h1>" + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
                    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
                    var responseHttp = "HTTP/1.1 200 OK" + HttpConstants.NewLine +
                        "Server: SoftUniBasicWebServer 1.0" + HttpConstants.NewLine +
                        "Content-Type: text/html" + HttpConstants.NewLine +
                        "Content-Length" + responseBodyBytes.Length + HttpConstants.NewLine +
                        HttpConstants.NewLine;
                    var reponseHeaderBytes = Encoding.UTF8.GetBytes(responseHttp);
                    await stream.WriteAsync(reponseHeaderBytes);
                    await stream.WriteAsync(responseBodyBytes);
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
