namespace Ecommerce.Models.ViewModels {
    public class HomeViewModel {
        public ICollection<Product> Products { get; set; }
        public Cart Cart { get; set; }  
    }
}
