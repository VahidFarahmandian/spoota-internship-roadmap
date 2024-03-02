using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principles.Solid
{

    // Open-Closed Principle (OCP) AND Liskov Substitution Principle (LSP)
    public abstract class Human
    {
        public abstract double CalculateInsurance(int age, string gender, double salary);

    }
    public class Employee : Human
    {
        public override double CalculateInsurance(int age, string gender, double salary)
        {
            double result = 0;
            if (gender == "Man")
            {
                if (age > 60)
                {
                    result = (salary * 12) + 100;
                }
                if (age < 60)
                {
                    result = (salary * 12) + 150;
                }
            }
            else if (gender == "Woman")
            {
                if (age > 60)
                {
                    result = (salary * 12) + 80;
                }
                if (age < 60)
                {
                    result = (salary * 12) + 110;
                }
                
            }
            return result;
        }
    }

    public class Nurse : Human
    {
        public override double CalculateInsurance(int age, string gender, double salary)
        {
            double result = 0;
            if (gender == "Man")
            {
                if (age > 60)
                {
                    result = (salary * 12) + 170;
                }
                if (age < 60)
                {
                    result = (salary * 12) + 200;
                }
            }
            else if (gender == "Woman")
            {
                if (age > 60)
                {
                    result = (salary * 12) + 150;
                }
                if (age < 60)
                {
                    result = (salary * 12) + 180;
                }

            }
            return result + 500;
        }
    }
    // Single Responsibility Principle (SRP)
    public class PayInsurance
    {
        List<string> Names;
        List<int> Years;
        public PayInsurance(List<int> Years, List<string> Names)
        {
            this.Names = Names;
            this.Years = Years;
        }
            
        string InsuranceRenewal(int id, string name)
        {
            string status = "";
            if (Names.ElementAt(id) == name)
            {

               int YearOfId=Years.ElementAt(id);
               if (YearOfId == 10)
                {
                    status = "Already It has been extended";
                    return status;
                }    
               YearOfId = YearOfId + 10;
               status = "Successful";
            }
            else
                status = "Not correct user";
            return status;

        }

    }
    public class InvokeInsurance
    {
        List<string> Names;
        List<int> Years;
        public InvokeInsurance(List<int> Years, List<string> Names)
        {
            this.Names = Names;
            this.Years = Years;
        }

        string InsuranceRenewal(int id, string name)
        {
            string status = "";
            if (Names.ElementAt(id) == name)
            {

                int YearOfId = Years.ElementAt(id);
                if (YearOfId == 0)
                {
                    status = "Already finished";
                    return status;
                }
                YearOfId = 0;
                status = "Successful";
                Names.RemoveAt(id);
                Years.RemoveAt(id);
            }
            else
                status = "Not correct user";
            return status;

        }

    }
}
