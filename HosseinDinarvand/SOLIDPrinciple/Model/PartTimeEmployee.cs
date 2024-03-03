using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple.Model
{
    public class PartTimeEmployee : Employee
    {
        public override decimal Salary
        {
            get { return base.Salary }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Salary cannot be negative for part-time employees.");
                }

                base.Salary = value;
            }
        }
    }
}
