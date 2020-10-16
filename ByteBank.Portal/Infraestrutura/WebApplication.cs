using ByteBank.Portal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;

        public WebApplication(string[] prefixos)
        {
            this._prefixos = prefixos ?? throw new ArgumentException(nameof(prefixos));
        }

        public void Iniciar()
        {
            while (true)
                ManipularRequisicoes();
        }

        private void ManipularRequisicoes()
        {
            var httpListener = new HttpListener();
            foreach (var prefixo in _prefixos)
                httpListener.Prefixes.Add(prefixo);
            httpListener.Start();
            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;
            var path = requisicao.Url.PathAndQuery;
            if (Utilidades.EhArquivo(path))
            {
                var manipulador = new ManipuladorRequisicaoArquivo();
                manipulador.Manipular(resposta, path);
            }
            else
            {
                var manipulador = new ManipuladorRequisicaoController();
                manipulador.Manipular(resposta, path);
            }
            httpListener.Stop();
        }
    }
}
