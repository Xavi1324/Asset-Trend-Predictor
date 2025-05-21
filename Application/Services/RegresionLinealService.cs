using Application.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegresionLinealService
    {
        public ResultadoRegresionDto Calcular(List<double> y)
        {
            int n = y.Count;
            var x = Enumerable.Range(0, n).Select(i => (double)i).ToList();

            double sumX = x.Sum(), sumY = y.Sum();
            double sumX2 = x.Sum(xi => xi * xi);
            double sumXY = x.Zip(y, (xi, yi) => xi * yi).Sum();

            double m = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
            double b = (sumY - m * sumX) / n;

            return new ResultadoRegresionDto
            {
                Pendiente = m,
                ValorEstimadoDia21 = m * 20 + b,
                Tendencia = m > 0 ? "Alcista 📈" : "Bajista 📉"
            };
        }
    }
}

