using Dapper;
using NetProject.model;
using System.Data;
using System.Data.SqlClient;

public interface IRepository
{
    IEnumerable<User> GetAllUsers();
}

public class DapperRepository : IRepository
{
    private readonly SqlConnection _connection;

    public DapperRepository(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public IEnumerable<User> GetAllUsers()
    {
        _connection.Open();
        var sql = "select * from MyProperty"; 
        return _connection.Query<User>(sql);
    }
}
