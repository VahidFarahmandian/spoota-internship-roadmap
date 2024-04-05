using SOLIDPrinciple.Employee;
using SOLIDPrinciple.Model;
using SOLIDPrinciple.Repository;

namespace SOLIDPrinciple.Manager
{
    public class EmployeeManager : IEmployyManager, IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        private List<Model.Employee> employees = new List<Model.Employee>();
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeManager() { }

        public EmployeeManager(IEmployeeRepository repository)
        {
            this.employeeRepository = repository;
        }


        public void AddEmployee(Model.Employee employee)
        {
            employeeRepository.AddEmployee(employee);
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
