using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GoMarket.Models
{
    public class NombreDeTiendasVM
    {
        [Display(Name = "Store name")]
        public string NombreTienda { get; set; }
        [Display(Name = "Amount")]
        public int Cantidad { get; set; }
    }
}
