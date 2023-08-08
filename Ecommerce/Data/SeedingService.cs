using Ecommerce.Models;

namespace Ecommerce.Data {
    public class SeedingService {
        private EcommerceContext _context;
        public SeedingService(EcommerceContext context) {
            _context = context;
        }

        public void Seed() {
            if (_context.Products.Any() || _context.Promotions.Any()) {
                return; // Banco de dados já possui dados
            }

            Promotion p0 = new Promotion("Nenhuma");
            Promotion p1 = new Promotion("Leve 2 e Pague 1");
            Promotion p2 = new Promotion("3 por R$ 10,00");

            _context.AddRange(p0, p1, p2);
            _context.SaveChanges();
        }
    }
}
