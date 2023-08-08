using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services {
    public class ProductService {
        private readonly EcommerceContext _context;
        public ProductService(EcommerceContext context) {
            _context = context;
        }

        public async Task<List<Product>> FindAllAsync() {
            return await _context.Products.ToListAsync();
        }

        public async Task InsertAsync(Product product) {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> FindByIdAsync(int id) {
            return await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task RemoveAsync(int id) {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product) {
            bool hasAny = await _context.Products.AnyAsync(x => x.Id == product.Id);
            if (!hasAny) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
