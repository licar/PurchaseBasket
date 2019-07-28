using System.ComponentModel.DataAnnotations;

namespace PurchaseBasket.Models
{
    public class ProductModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Weight { get; set; } = 0;

    }
}