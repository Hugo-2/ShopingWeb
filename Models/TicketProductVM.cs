using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GoMarket.Models
{
    public class TicketProductVM
    {
        public int Id { get; set; }


        [Display(Name = "Product")]
        public string Producto { get; set; }


        [Display(Name = "Amount")]
        public int Cantidad { get; set; }


        [Display(Name = "Price")]
        public decimal? Precio { get; set; }

        public decimal SubTotal { get; set; }


        [Display(Name = "Market")]
        public string Tienda { get; set; }

    }
}
