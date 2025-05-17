using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class HistoricalDataInputModel
    {
        [Required(ErrorMessage = "El campo de datos es obligatorio.")]
        public  string HistoricalData { get; set; }

        public List<string> ProcesarDatos()
        {
            return HistoricalData?
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToList() ?? new List<string>();
        }

        public bool TieneCantidadValidaDeLineas()
        {
            var lines = ProcesarDatos();
            return lines.Count == 20;
        }
    }
}
