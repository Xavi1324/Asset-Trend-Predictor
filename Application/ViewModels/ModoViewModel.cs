using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ModoViewModel
    {
        [Required(ErrorMessage = "Debes seleccionar un modo de predicción.")]
        public int? OpcionSeleccionada { get; set; }
    }
}
