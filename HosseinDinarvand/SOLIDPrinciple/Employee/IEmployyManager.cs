using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple.Employee
{
    public interface IEmployyManager
    {
        void AddEmployee(Model.Employee employee);
        void PrintEmpolyeeDetails(List<Model.Employee> employees);
        void PrintFilteredEmployeeDatails(EmployeeFilter filter);
    }
}
