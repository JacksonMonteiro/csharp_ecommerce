namespace Ecommerce.Models {
    public class CartItem {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double CalculateTotalPrice() {
            if (isPromotionApplicable(Product.Promotion, Quantity)) {
                return applyPromotion(Product, Quantity);
            }
            else {
                return Product.Price * Quantity;
            }
        }

        public bool isPromotionApplicable(Promotion promotion, int quantity) {
            if (promotion.Title.Equals("Leve 2 e Pague 1") && quantity % 2 == 0) {
                return true;
            }

            if (promotion.Title.Equals("3 por R$ 10,00") && quantity % 3 == 0) {
                return true;
            }

            return false;
        }

        public double applyPromotion(Product product, int quantity) {
            if (product.Promotion.Title.Equals("Leve 2 e Pague 1")) {
                int finalQuantity = quantity / 2;
                return (product.Price * finalQuantity);
            }

            if (product.Promotion.Title.Equals("3 por R$ 10,00")) {
                int finalQuantity = quantity / 3;
                return (10.00 * finalQuantity);
            }

            return 0.0;
        }
    }
}
