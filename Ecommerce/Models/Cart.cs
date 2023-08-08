namespace Ecommerce.Models {
    public class Cart {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddProduct(Product product) {
            var existingItem = Items.Find(item => item.Product.Id == product.Id);

            if (existingItem != null) {
                existingItem.Quantity += 1;
            }
            else {
                var newItem = new CartItem() { Product = product, Quantity = 1 };
                Items.Add(newItem);
            }
        }

        public void RemoveItem(Product product) {
            Items.RemoveAll(item => item.Product.Id == product.Id);
        }

        public void UpdateItem(Product product) {
            var item = Items.FirstOrDefault(item => item.Product.Id == product.Id);
            
            if (item != null) {
                item.Product = product;
            }
        }

        public void IncreaseQuantity(Product product) {
            var existingItem = Items.Find(item => item.Product.Id == product.Id);
            existingItem.Quantity += 1;
        }
        public void DecreaseQuantity(Product product) {
            var existingItem = Items.Find(item => item.Product.Id == product.Id);

            if (existingItem.Quantity > 1) {
                existingItem.Quantity -= 1;
            }
            else {
                RemoveItem(existingItem.Product);
            }
        }

        public double CalculateTotalValue() {
            double total = 0;

            foreach (var item in Items) {
                total += item.CalculateTotalPrice();
            }

            return total;
        }
    }
}
