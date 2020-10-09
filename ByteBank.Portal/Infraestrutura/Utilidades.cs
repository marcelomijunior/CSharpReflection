using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infraestrutura
{
    public static class Utilidades
    {
        public static bool EhArquivo(string path)
        {
            var partesPath = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var ultimaParte = partesPath.Last();
            return ultimaParte.Contains(".");
        }

        public static string ConverterPathParaNomeAssembly(string path)
        {
            var prefixoAssembly = "Bytebank_01";
            var pathComPontos = path.Replace("/", ".");
            var pathCompleto = $"{prefixoAssembly}{pathComPontos}";

            return pathCompleto;
        }

        public static string ObterTipoDeConteudoDoArquivo(string path)
        {
            if (path.EndsWith("css"))
                return "text/css; charset=UTF-8";
            if (path.EndsWith("html"))
                return "text/html; charset=UTF-8";
            if (path.EndsWith("js"))
                return "application/js; charset=UTF-8";

            throw new NotImplementedException("Tipo de conteúdo não encontrado.");
        }
    }
}
