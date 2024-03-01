using FirstWeb.API.Model.Domain;

namespace FirstWeb.API.Repositories
{
    public interface IProductRepositoryEFCore
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> DeleteAsync(int id);
    }
}
