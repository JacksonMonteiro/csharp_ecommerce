namespace Ecommerce.Models {
    public class Promotion {
        public int Id { get; set; }
        public string Title { get; set; }

        public Promotion() { }

        public Promotion(string title) {
            Title = title;
        }
    }
}
