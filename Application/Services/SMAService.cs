using Application.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SMAService
    {
        public ResultadoSmaDto Calcular(List<double> valores)
        {
            var smaCorta = valores.TakeLast(5).Average();
            var smaLarga = valores.Average();

            return new ResultadoSmaDto
            {
                SmaCorta = smaCorta,
                SmaLarga = smaLarga,
                Tendencia = smaCorta > smaLarga ? "Alcista 📈" : "Bajista 📉"
            };
        }

    }
}

