using GoMarket.Models;
using GoMarket.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GoMarket.Controllers
{
    public class TicketController : Controller
    {
        private readonly IRepositorioListaCompras repositorioListaCompras;

        public TicketController( IRepositorioListaCompras repositorioListaCompras)
        {
            this.repositorioListaCompras = repositorioListaCompras;
        }

        /***************************************************************************************************/


        // GET ----------------------------------------------
        public async Task<IActionResult> Ticket(string nomTienda)
        {
            var productos = await repositorioListaCompras.GetProductosComprar(nomTienda);

            ViewBag.tienda = nomTienda;

            return View(productos);
        }



        // GET ----------------------------------------------
        public IActionResult addOneMore(string shopName)
        {
            ViewBag.ShopName = shopName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addOneMore(TicketProductVM product)
        {
            var res = await repositorioListaCompras.InsertNewProductTicket(product);

            return RedirectToAction("Ticket",new { nomTienda = product.Tienda});
        }



        // GET ----------------------------------------------
        public async Task<IActionResult> editProduct(int idProducto)
        {
            var producto = await repositorioListaCompras.GetProductoEditar(idProducto);

            return View(producto);  
        }

        [HttpPost]
        public async Task<IActionResult> editProduct(TicketProductVM prod)
        {
            var res = await repositorioListaCompras.EditarProductoTicket(prod);

            if (res)
                return RedirectToAction("Ticket", "Ticket", new { nomTienda = prod.Tienda });

            return View();
        }



        // GET ---------------------------------------
        [HttpPost]
        public async Task<IActionResult> BorrarProductoTicket(TicketProductVM productoBorrar)
        {
            await repositorioListaCompras.BorrarListaRecordatorio(productoBorrar.Id);

            return RedirectToAction("Ticket", "Ticket", new { nomTienda = productoBorrar.Tienda});
        }

    }
}
