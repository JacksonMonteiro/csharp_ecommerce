using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models {
    public class Product {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} do produto é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} do produto é obrigatório")]
        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Price { get; set; }
        public Promotion? Promotion { get; set; }
        [Display(Name = "Promoção")]
        public int PromotionId { get; set; }

        public Product() { }

        public Product(string name, double price) {
            Name = name;
            Price = price;
        }

        public Product(string name, double price, Promotion? promotion) {
            Name = name;
            Price = price;
            Promotion = promotion;
        }
    }
}
