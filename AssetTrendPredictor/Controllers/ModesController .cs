using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrendPredictor.Controllers
{
    public class ModesController : Controller
    {
        [HttpGet]
        public ActionResult PredictionModes()
        {
            var opcionGuardada = SeleccionModesService.Instance.OpcionSeleccionada;
            int? opcionSeleccionada = opcionGuardada >= 0 && opcionGuardada <= 2 ? opcionGuardada : null;
            var modelo = new ModoViewModel
            {
                OpcionSeleccionada = opcionSeleccionada
            };
            return View(modelo);
        }
        [HttpPost]
        public ActionResult PredictionModes(ModoViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                SeleccionModesService.Instance.OpcionSeleccionada = modelo.OpcionSeleccionada;
                TempData["MensajeExito"] = "Modo de predicción guardado exitosamente.";
                return RedirectToAction("PredictionModes");
            }

            return View(modelo); 
        }


    }
}
