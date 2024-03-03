namespace SOLIDPrinciple.Employee
{
    public interface IEmployyManager
    {
        void AddEmployee(Model.Employee employee);
        void PrintEmpolyeeDetails(List<Model.Employee> employees);
        void PrintFilteredEmployeeDatails(EmployeeFilter filter);
    }
}
