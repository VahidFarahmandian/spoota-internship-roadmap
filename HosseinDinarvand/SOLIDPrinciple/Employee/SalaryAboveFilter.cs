namespace SOLIDPrinciple.Employee
{
    public class SalaryAboveFilter : EmployeeFilter
    {
        private decimal _threshould;
        public SalaryAboveFilter(decimal threshould)
        {
            this._threshould = threshould;
        }
        public override List<Model.Employee> Filter(List<Model.Employee> employees)
        {
            List<Model.Employee> filterdEmployees = new List<Model.Employee>();
            foreach (var employee in employees)
            {
                if (employee.Salary > _threshould)
                    filterdEmployees.Add(employee);
            }

            return filterdEmployees;
        }
    }
}
