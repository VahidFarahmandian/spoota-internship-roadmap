namespace SOLIDPrinciple.Model
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual decimal Salary { get; set; }
    }
}
