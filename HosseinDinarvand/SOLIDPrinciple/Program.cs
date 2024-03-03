using SOLIDPrinciple.Manager;
using SOLIDPrinciple.Model;

class Program 
{
    static void Main(string[] args)
    {
        EmployeeManager manager = new EmployeeManager();

        manager.AddEmployee(new Employee { Id = 1, Name = "John" });
        manager.AddEmployee(new Employee { Id = 2, Name = "Jack" });

        Console.ReadKey();
    }
}
