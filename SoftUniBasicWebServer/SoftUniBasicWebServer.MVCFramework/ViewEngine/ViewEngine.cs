﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework.ViewEngine
{
    public class ViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            var csharpCode = GenerateCSharpFromTemplate(templateCode);
            var executableObject = GenerateExecutableCode(csharpCode, viewModel);
            var html = executableObject.ExecuteTemplate(viewModel);
            return html;
        }
        private string GenerateCSharpFromTemplate(string templateCode)
        {
            string csharpCode = @"
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using SoftUniBasicWebServer.MVCFramework;

namespace ViewNamespace
{
    public class ViewClass : IView
    {
        public string ExecuteTemplate(object viewModel)
        {
            var html = new StringBuilder();

            "+ GetMethodBody(templateCode) + @" 
            
            return html.ToString();
        }
    }
}
";
            return csharpCode;
        }
        private IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            var compileResult = CSharpCompilation.Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));
            if (viewModel != null)
            {
                compileResult = compileResult.AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }
            var libraries = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                compileResult = compileResult.AddReferences(MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }

            compileResult = compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                EmitResult result = compileResult.Emit("view.dll");
                if (!result.Success)
                {
                    return new ErrorView(result.Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error).Select(x => x.GetMessage()), csharpCode);
                }
                try
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var byteAssembly = memoryStream.ToArray();
                    var assembly = Assembly.Load(byteAssembly);
                    var viewType = assembly.GetType("ViewNamespace.ViewClass");
                    var instance = Activator.CreateInstance(viewType);
                    return instance as IView;
                }
                catch (Exception ex)
                {
                    return new ErrorView(new List<string> { ex.ToString() }, csharpCode);
                }
            }
        }
        private string GetMethodBody(string templateCode)
        {
            StringReader sr = new StringReader(templateCode);
            StringBuilder csharpCode = new StringBuilder();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                csharpCode.AppendLine($"html.AppendLine(@\"{line.Replace("\"", "\"\"")}\");");
            }
            return string.Empty;
        }
    }
}
