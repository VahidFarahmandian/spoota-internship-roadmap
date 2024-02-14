using Dapper;
using FirstWeb.API.Model.Domain;
using Microsoft.Data.SqlClient;

namespace FirstWeb.API.Repositories.Dapper
{
    public class SQLProductRepositoryDapper : IProductRepoitoryDapper
    {
        private readonly IConfiguration configuration;
        public SQLProductRepositoryDapper(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            var products = await connection.QueryAsync<Product>("SELECT * FROM Products");
            return products;
        }
        
        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            var product = await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id",new {Id = id});
            return product;
        }

    }
}
