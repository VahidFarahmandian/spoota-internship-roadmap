using Dapper;
using NetProject.model;
using System.Data;
using System.Data.SqlClient;

public class DapperRepository
{
    private readonly string _connectionString;

    public DapperRepository(string connectionString)
    {
        _connectionString = connectionString;

      
    }

    public IEnumerable<User> GetAllUsers()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = "SELECT * FROM YourActualUserTable"; // Replace with your actual table name
        return connection.Query<User>(sql);
    }
}
