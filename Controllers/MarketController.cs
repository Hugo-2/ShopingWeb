using GoMarket.Models;
using GoMarket.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GoMarket.Controllers
{
    public class MarketController : Controller
    {
        private readonly IRepositorioListaCompras repositorioListaCompras;

        public MarketController(IRepositorioListaCompras repositorioListaCompras)
        {
            this.repositorioListaCompras = repositorioListaCompras;
        }

        /**********************************************************************************************/


        // GET ---------------------------------------
        public async Task<IActionResult> Recordatorio()
        {
            var productos = await repositorioListaCompras.GetRecordarCompra();

            return View(productos);
        }



        // GET ----------------------------------------
        public async Task<IActionResult> EditarRecordatorio(int idProductoLista)
        {
            var producto = await repositorioListaCompras.GetListaEditar(idProductoLista);
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRecordatorio(ProductosVM prod)
        {
            var correccion = await repositorioListaCompras.EditarListaRecordatorio(prod);

            if (correccion)
            {
                return RedirectToAction("Recordatorio", "Market");
            }

            return View();
        }



        // GET ---------------------------------------
        [HttpPost]
        public async Task<IActionResult> BorrarRecordatorio(ProductosVM productoBorrar)
        {
            await repositorioListaCompras.BorrarListaRecordatorio(productoBorrar.Id);

            return RedirectToAction("Recordatorio");
        }



        // GET ---------------------------------------
        public IActionResult AddProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProducto(ProductosVM prod)
        {
            var registrado = await repositorioListaCompras.InsertNewProduct(prod);

            if (registrado)
            {
                return RedirectToAction("Recordatorio","Market");
            } 
            else
            {
                return View(prod);
            }
        }


    }
}
