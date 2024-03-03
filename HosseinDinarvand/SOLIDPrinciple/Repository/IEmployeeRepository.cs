using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple.Repository
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Model.Employee employee);
    }
}
