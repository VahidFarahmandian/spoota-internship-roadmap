using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principles.Dry
{
    public interface IPerson
    {
        public bool Validate();
    }
    public class Client:IPerson
    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string passwrd { get; set; }
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Console.WriteLine("Please provide a valid name.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                Console.WriteLine("Please provide a valid email address.");
                return false;
            }

            return true;
        }
    }
    public class Seller : IPerson

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string passwrd { get; set; }
        public string address { get; set; }
        public bool Validate()
        {

            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Not a valid  address.");
                return false;
            }
            if (!Email.Contains("@"))
            {
                Console.WriteLine("Not a valid email .");
                return false;
            }    
            if (passwrd.Length < 10)
            {
                Console.WriteLine("Not a valid password At least 10 characters .");
                return false;
            }

            return true;
        }
    }
    public class ClientRepository
    {
        public void SaveCustomer(IPerson person)
        {
            if (person.Validate())
            {
                Console.WriteLine("Person saved successfully.");
            }
        }
    }
}
