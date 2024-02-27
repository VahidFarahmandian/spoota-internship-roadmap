using FirstWeb.API.Model.Domain;

namespace FirstWeb.API.Repositories.Dapper
{
    public interface IProductRepoitoryDapper
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
    }
}
