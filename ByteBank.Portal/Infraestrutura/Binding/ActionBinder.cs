﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infraestrutura.Binding
{
    public class ActionBinder
    {
        public ActionBindInfo ObterActionBindInfo(object controller, string path)
        {
            var idxInterrogacao = path.IndexOf('?');
            var existeQueryString = idxInterrogacao >= 0;
            if (!existeQueryString)
            {
                var nomeAction = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var methodInfo = controller.GetType().GetMethod(nomeAction);
                return new ActionBindInfo(methodInfo, Enumerable.Empty<ArgumentoNomeValor>());
            }
            else
            {
                var nomeControllerComAction = path.Substring(0, idxInterrogacao);
                var nomeAction = nomeControllerComAction.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                var queryString = path.Substring(idxInterrogacao + 1);
                var tuplasNomeValor = ObterArgumentosNomeValor(queryString);
                var nomeArgumentos = tuplasNomeValor.Select(tupla => tupla.Nome).ToArray();
                var methodInfo = ObterMethodInfoApartirDeNomeEArgumentos(nomeAction, nomeArgumentos, controller);
                return new ActionBindInfo(methodInfo, tuplasNomeValor);
            }
        }

        private IEnumerable<ArgumentoNomeValor> ObterArgumentosNomeValor(string queryStrings)
        {
            var tuplasNomeValor = queryStrings.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tupla in tuplasNomeValor)
            {
                var partesTupla = tupla.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                yield return new ArgumentoNomeValor(partesTupla[0], partesTupla[1]);
            }
        }

        private MethodInfo ObterMethodInfoApartirDeNomeEArgumentos(string nomeAction, string[] argumentos, object controller)
        {
            int agumentosCout = argumentos.Length;
            BindingFlags bindFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var metodos = controller.GetType().GetMethods(bindFlags);
            var sobrecargas = metodos.Where(metodo => metodo.Name == nomeAction);
            foreach (var sobrecarga in sobrecargas)
            {
                var parametros = sobrecarga.GetParameters();
                if (agumentosCout != parametros.Length) continue;
                var match = parametros.All(parametro => argumentos.Contains(parametro.Name));
                if (match) return sobrecarga;
            }
            throw new ArgumentException($"A sobrecarga do método {nomeAction} não foi encontrada!");
        }
    }
}
