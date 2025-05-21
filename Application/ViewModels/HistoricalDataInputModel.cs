using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class HistoricalDataInputModel
    {
        [Required(ErrorMessage = "El campo de datos es obligatorio.")]
        public  string HistoricalData { get; set; }

    }
}
