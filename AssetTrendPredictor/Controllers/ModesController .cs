using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrendPredictor.Controllers
{
    public class ModesController : Controller
    {
        
        public ActionResult PredictionModes()
        {
            return View();
        }

        
    }
}
