namespace SOLIDPrinciple.Model
{
    public class FullTimeEmployee : Employee
    {
        public override decimal Salary
        {
            get { return base.Salary; }
            set { base.Salary = value; }
        }
    }
}
