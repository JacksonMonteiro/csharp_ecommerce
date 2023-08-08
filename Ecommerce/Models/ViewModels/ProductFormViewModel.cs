namespace Ecommerce.Models.ViewModels {
    public class ProductFormViewModel {
        public Product Product { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}
