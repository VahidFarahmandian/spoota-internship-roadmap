using FirstWeb.API.Data;
using FirstWeb.API.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.API.Repositories
{
    public class SQLProductRepositoryEFCore : IProductRepositoryEFCore
    {
        private readonly ApplicationDbContext dbContext;
        public SQLProductRepositoryEFCore(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<Product?> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var existInProducts = await dbContext.Products.FindAsync(id);

            if (existInProducts == null)
                return null;

            dbContext.Products.Remove(existInProducts);
            await dbContext.SaveChangesAsync();
            return existInProducts;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existInProducts = await dbContext.Products.FindAsync(id);

            if (existInProducts == null)
                return null;

            existInProducts.Name = product.Name;
            existInProducts.Category = product.Category;
            existInProducts.Price = product.Price;

            await dbContext.SaveChangesAsync();
            return existInProducts;
        }
    }
}
