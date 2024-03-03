using ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUserById(int Id);
    }
}
