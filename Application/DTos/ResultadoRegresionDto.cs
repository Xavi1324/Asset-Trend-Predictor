using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTos
{
    public class ResultadoRegresionDto
    {
        public double Pendiente { get; set; }
        public double ValorEstimadoDia21 { get; set; }
        public string Tendencia { get; set; }
    }
}
