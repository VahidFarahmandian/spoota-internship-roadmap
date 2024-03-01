using Dapper;
using FirstWeb.API.Model.Domain;
using Microsoft.Data.SqlClient;

namespace FirstWeb.API.Repositories.Dapper
{
    public class SQLProductRepositoryDapper : IProductRepoitoryDapper
    {
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection;
        public SQLProductRepositoryDapper(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await connection.QueryAsync<Product>("SELECT * FROM Products");
            return products;
        }
        
        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id",new {Id = id});
            return product;
        }

    }
}
