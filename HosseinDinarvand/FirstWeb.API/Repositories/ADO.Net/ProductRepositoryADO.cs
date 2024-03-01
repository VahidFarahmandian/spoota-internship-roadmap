using FirstWeb.API.Model.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FirstWeb.API.Repositories.ADO.Net
{
    public class ProductRepositoryADO : IProductRepositoryADO
    {
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection;
        private readonly Product product;
        public ProductRepositoryADO(IConfiguration _configuration, Product _product)
        {
            this.configuration = _configuration;
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            this.product = _product; 
        }
        public Product? getByName(string name)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Products WHERE Name = @Name", connection);
            //parameterize query
            command.Parameters.AddWithValue("@Name", name);
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
