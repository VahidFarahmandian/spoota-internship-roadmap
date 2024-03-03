using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void AddEmployee(Model.Employee employee)
        {
            Console.WriteLine($"employee {employee.Name} added.");
        }
    }
}
