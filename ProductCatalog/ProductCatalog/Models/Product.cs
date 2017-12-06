using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Model
{
    public class Product
    {

        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Product Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

    }
}
