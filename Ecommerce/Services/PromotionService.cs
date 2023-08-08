using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services {
    public class PromotionService {
        private readonly EcommerceContext _context;
        public PromotionService(EcommerceContext context) {
            _context = context;
        }

        public async Task<List<Promotion>> FindAllAsync() {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion> FindByIdAsync(int id) {
            return await _context.Promotions.FirstOrDefaultAsync(promotion => promotion.Id == id);
        }
    }
}
