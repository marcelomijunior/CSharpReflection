using ByteBank.Service.Services.Interfaces;
using System;

namespace ByteBank.Service.Services
{
    public class CambioService : ICambioService
    {
        private readonly Random random = new Random();

        public decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor)
        {
            return (decimal)random.NextDouble();
        }
    }
}
