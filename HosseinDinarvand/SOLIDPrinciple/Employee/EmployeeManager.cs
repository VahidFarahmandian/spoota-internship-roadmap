using SOLIDPrinciple.Employee;
using SOLIDPrinciple.Model;

namespace SOLIDPrinciple.Manager
{
    public class EmployeeManager : IEmployyManager, IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        private List<Model.Employee> employees = new List<Model.Employee>();


        public void AddEmployee(Model.Employee employee)
        {
            employees.Add(employee);
            Console.WriteLine("Employee addeds successfuly.");
        }

        public void PrintEmpolyeeDetails(List<Model.Employee> employees)
        {
            foreach (var employee in employees)
                Console.WriteLine($"Id:{employee.Id} ,Name:{employee.Name} ,Salary:{employee.Salary}");
        }

        public void PrintFilteredEmployeeDatails(EmployeeFilter filter)
        {
            var filterEmployees = filter.Filter(employees);
            PrintEmpolyeeDetails(filterEmployees);
        }

    }
}
