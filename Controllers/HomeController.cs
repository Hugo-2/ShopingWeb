using GoMarket.Models;
using GoMarket.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace GoMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioListaCompras repositorioListaCompras;

        public HomeController(ILogger<HomeController> logger, 
            IRepositorioListaCompras repositorioListaCompras)
        {
            _logger = logger;
            this.repositorioListaCompras = repositorioListaCompras;
        }


        /****************************************************************************************************/


        // GET ----------------------------------
        public async Task<IActionResult> Index()
        {
            var modelTiendas = await repositorioListaCompras.GetNombreTiendas();
                      
            return View(modelTiendas);
        }

        [HttpPost]
        public IActionResult Index(string listaDeTiendas)
        {

            var data = listaDeTiendas;
            //return RedirectToAction("Comprar","Market",new {data = data });
            return RedirectToAction("Comprar","Market",new {data = data });
        }









        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}