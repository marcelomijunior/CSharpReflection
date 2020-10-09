using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infraestrutura
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName] string nomeArquivo = null)
        {
            var type = GetType();
            var diretorioNome = type.Name.Replace("Controller", "");
            var nomeCompleto = $"ByteBank.Portal.View.{diretorioNome}.{nomeArquivo}.html";
            var assembly = Assembly.GetExecutingAssembly();
            var streamResourcer = assembly.GetManifestResourceStream(nomeCompleto);
            var streamLeitura = new StreamReader(streamResourcer);
            var textoPagina = streamLeitura.ReadToEnd();
            return textoPagina;
        }
    }
}
