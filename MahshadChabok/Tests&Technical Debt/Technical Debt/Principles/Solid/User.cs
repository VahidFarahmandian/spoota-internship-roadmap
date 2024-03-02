using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principles.Solid
{
    internal class User
    {
        public int id ;

        public string username;
        public string password; 
        public User(string username, string password)
        {
            this.username = username;   
            this.password = password;
        }
        public void SetId(int id) { 
            this.id = id;   
        }
    }
}
