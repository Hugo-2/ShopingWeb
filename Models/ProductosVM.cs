using System.ComponentModel.DataAnnotations;

namespace GoMarket.Models
{
    public class ProductosVM
    {
        public int Id { get; set; }


        [Display(Name="Product")]
        public string Producto { get; set; }
        
        
        [Display(Name = "Amount")]
        public int Cantidad { get; set; }
        
        
        [Display(Name = "Price")]
        public decimal? Precio { get; set; }
        
        public decimal SubTotal { get; set; }
        
        
        [Display(Name = "Store")]
        public string Tienda { get; set; }
        
        
        [Display(Name = "Place")]
        public string? Lugar { get; set; }
    }
}
