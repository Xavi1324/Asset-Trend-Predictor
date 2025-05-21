using Application.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MomentumService
    {
        public ResultadoMomentumDto Calcular(List<double> valores, int periodo = 5)
        {
            var dto = new ResultadoMomentumDto();

            for (int t = 0; t < valores.Count; t++)
            {
                string linea;
                if (t < periodo)
                    linea = $"Dia={t}, Precio={valores[t]:F2}, ROC({periodo})=n/a";
                else
                {
                    var roc = ((valores[t] / valores[t - periodo]) - 1) * 100;
                    linea = $"Dia={t}, Precio={valores[t]:F2}, ROC({periodo})={roc:F2}%";
                }
                dto.Resultados.Add(linea);
            }

            return dto;
        }
    }
}
