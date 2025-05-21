using Application.DTos;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrendPredictor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataProcesService _service;
        private readonly SMAService _sMAService;
        private readonly RegresionLinealService _regresionLinealService;
        private readonly MomentumService _momentumService;

        public HomeController(ILogger<HomeController> logger , DataProcesService service, SMAService sMAService, RegresionLinealService regresionLinealService,MomentumService momentumService )
        {
            _logger = logger;
            _service = service;
            _sMAService = sMAService;
            _regresionLinealService = regresionLinealService;
            _momentumService = momentumService;

        }

        public IActionResult Index()
        {
            var model = new HistoricalDataInputModel();

            if (TempData["DatosHistoricos"] != null)
            {
                model.HistoricalData = TempData["DatosHistoricos"].ToString();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(HistoricalDataInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var lineas = _service.ProcesarDatos(model.HistoricalData);

            if (!_service.TieneCantidadValidaDeLineas(lineas))
            {
                ViewData["CustomError"] = "Debe ingresar exactamente 20 líneas.";
                return View(model);
            }

            if (SeleccionModesService.Instance.OpcionSeleccionada == null)
            {
                ViewData["CustomError"] = "Debes seleccionar un modo de predicción antes de calcular.";
                return View(model);
            }

            var valores = _service.ExtraerValoresNumericos(lineas);
            int modo = SeleccionModesService.Instance.OpcionSeleccionada.Value;

            string resultado;

            switch (modo)
            {
                case 0: // SMA
                    ResultadoSmaDto sma = _sMAService.Calcular(valores);
                    resultado = $"SMA Corta: {sma.SmaCorta:F2}<br/>" +
                                $"SMA Larga: {sma.SmaLarga:F2}<br/>" +
                                $"Tendencia: {sma.Tendencia}";
                    break;

                case 1: // Regresión lineal
                    ResultadoRegresionDto reg = _regresionLinealService.Calcular(valores);
                    resultado = $"Pendiente: {reg.Pendiente:F4}<br/>" +
                                $"Valor estimado para el día 21: {reg.ValorEstimadoDia21:F2}<br/>" +
                                $"Tendencia: {reg.Tendencia}";
                    break;

                case 2: // Momentum (ROC)
                    ResultadoMomentumDto roc = _momentumService.Calcular(valores, 5);
                    resultado = string.Join("<br/>", roc.Resultados);
                    break;

                default:
                    resultado = "Modo de predicción no válido.";
                    break;
            }

            ViewData["Resultado"] = resultado;
            TempData["DatosHistoricos"] = model.HistoricalData;

            return View(model);
        }


    }
}
