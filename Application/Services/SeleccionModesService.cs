using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeleccionModesService
    {
        private static SeleccionModesService _instance;
        private static readonly object _lock = new object();

        public int? OpcionSeleccionada { get; set; }

        private SeleccionModesService() { }

        public static SeleccionModesService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new SeleccionModesService();
                    return _instance;
                }
            }
        }
    }
}
