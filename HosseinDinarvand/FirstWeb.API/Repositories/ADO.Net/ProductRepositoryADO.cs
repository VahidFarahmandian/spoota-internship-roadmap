using FirstWeb.API.Model.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FirstWeb.API.Repositories.ADO.Net
{
    public class ProductRepositoryADO : IProductRepositoryADO
    {
        private readonly IConfiguration configuration;
        public ProductRepositoryADO(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public Product? getByName(string name)
        {
            Product product = new Product();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            SqlCommand command = new SqlCommand($"SELECT * FROM Products WHERE Name = N'{name}'", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            product.Id = int.Parse(dataTable.Rows[0]["Id"].ToString());
            product.Name = dataTable.Rows[0]["Name"].ToString();
            product.Category = dataTable.Rows[0]["Category"].ToString();
            product.Price = decimal.Parse(dataTable.Rows[0]["Price"].ToString());

            return product;
        }
    }
}
