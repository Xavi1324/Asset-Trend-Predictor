using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Application.Services
{
    public class DataProcesService
    {
        
        public List<string> ProcesarDatos(string datos)
        {
            return datos?
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList() ?? new List<string>();
        }

        
        public bool TieneCantidadValidaDeLineas(List<string> lineas)
        {
            return lineas.Count == 20;
        }

        // Extrae valores numéricos de cada línea (acepta líneas con fecha o sin fecha)
        public List<double> ExtraerValoresNumericos(List<string> lineas)
        {
            var valores = new List<double>();

            foreach (var linea in lineas)
            {
                string valorTexto;

                // Si contiene una coma, se asume que hay fecha
                if (linea.Contains(','))
                {
                    var partes = linea.Split(',', 2, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length < 2)
                        throw new FormatException($"Línea inválida (faltan valores): '{linea}'");

                    valorTexto = partes[1];
                }
                else
                {
                    // Solo valor sin fecha
                    valorTexto = linea;
                }

                // Limpieza
                valorTexto = valorTexto
                    .Trim()
                    .Replace("\u00A0", "") // Elimina NBSP
                    .Replace(" ", "")
                    .Replace(",", ".");   // Normaliza coma decimal a punto

                if (!double.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
                {
                    throw new FormatException($"Valor numérico inválido en la línea: '{linea}'");
                }

                valores.Add(valor);
            }

            return valores;
        }
    }
}
