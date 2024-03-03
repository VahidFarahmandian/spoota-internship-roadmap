using SOLIDPrinciple.Employee;
using SOLIDPrinciple.Manager;
using SOLIDPrinciple.Model;

class Program 
{
    static void Main(string[] args)
    {
        EmployeeManager manager = new EmployeeManager();

        manager.AddEmployee(new Employee { Id = 1, Name = "John",Salary = 50000 });
        manager.AddEmployee(new Employee { Id = 2, Name = "Jack",Salary = 60000 });

        Console.WriteLine("------------------------------------------");

        Console.WriteLine("Filtered Employee Deatails (Salary above 55000)");
        manager.PrintFilteredEmployeeDatails(new SalaryAboveFilter(55000));

        Console.ReadKey();
    }
}
