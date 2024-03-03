using ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class UserService
    {
        private readonly IUserService userService;
        public UserService(IUserService _userService)
        {
            this.userService = _userService;
        }
        public void AddUser(User user)
        {
            userService.AddUser(user);
        }

        public User GetUserById(int Id)
        {
            return userService.GetUserById(Id);
        }
    }
}
