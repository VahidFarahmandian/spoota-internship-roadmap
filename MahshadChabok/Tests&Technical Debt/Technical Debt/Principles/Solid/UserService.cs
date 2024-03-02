using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principles.Solid
{
    // Dependency Inversion Principle (DIP)
    public interface IDataAccess
    {
        void SaveData(string data);
    }

    public class DataAccess : IDataAccess
    {
        LinkedList<string> Date= new LinkedList<string>();  
        public void SaveData(string data)
        {
           Date.AddLast(data);
        }
    }

    public class UserService
    {
        private readonly IDataAccess _dataAccess;

        public UserService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void CreateUser(string username, string password)
        {
            User user = new User(username, password);

            _dataAccess.SaveData("User created: " + username);
        }
    }
}
