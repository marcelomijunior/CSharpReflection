using ByteBank.Portal.Infraestrutura;
using ByteBank.Service.Services;
using ByteBank.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class CambioController : ControllerBase
    {
        private ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioService();
        }

        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "USD", 1);
            var textoPagina = View();
            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
            return textoResultado;
        }

        public string USD()
        {
            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
            var textoPagina = View();
            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
            return textoResultado;
        }

        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorFinal = _cambioService.Calcular(moedaOrigem, moedaDestino, valor);
            var textoPagina = View();
            //VALOR_MOEDA_ORIGEM  MOEDA_ORIGEM = VALOR_MOEDA_DESTINO MOEDA_DESTINO
            var textoResultado = textoPagina
                .Replace("VALOR_MOEDA_ORIGEM", valor.ToString())
                .Replace("VALOR_MOEDA_DESTINO", valorFinal.ToString())
                .Replace("MOEDA_ORIGEM", moedaOrigem.ToString())
                .Replace("MOEDA_DESTINO", moedaDestino.ToString());
            return textoResultado;
        }

        public string Calculo(string moedaDestino) => Calculo("BRL", moedaDestino, 1);
    }
}
