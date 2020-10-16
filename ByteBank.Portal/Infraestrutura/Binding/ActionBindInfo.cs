using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infraestrutura.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<ArgumentoNomeValor> TuplasArgumentoNomeValor { get; set; }

        public ActionBindInfo(MethodInfo methodInfo, IEnumerable<ArgumentoNomeValor> argumentosNomeValor)
        {
            MethodInfo = methodInfo ?? throw new ArgumentException(nameof(methodInfo));
            if (argumentosNomeValor == null) throw new ArgumentException(nameof(argumentosNomeValor));
            TuplasArgumentoNomeValor = new ReadOnlyCollection<ArgumentoNomeValor>(argumentosNomeValor.ToList());
        }

        public object Invoke(object controller)
        {
            var contagemDeArgumentos = TuplasArgumentoNomeValor.Count();
            var possuiArgumentos = contagemDeArgumentos > 0;
            if (!possuiArgumentos)
                MethodInfo.Invoke(controller, new object[] { });
            var parametrosInvoke = new object[contagemDeArgumentos];
            var parametrosMethodInfo = MethodInfo.GetParameters();
            for (int i = 0; i < contagemDeArgumentos; i++)
            {
                var parametro = parametrosMethodInfo[i];
                var parametroNome = parametro.Name;
                var argumento = TuplasArgumentoNomeValor.Single(tupla => tupla.Nome == parametroNome);
                parametrosInvoke[i] = Convert.ChangeType(argumento.Valor, parametro.ParameterType);
            }
            return MethodInfo.Invoke(controller, parametrosInvoke);
        }
    }
}
