using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebApplication.Business_Logic.Interfaces;
using EmployeeWebApplication.Entites;

namespace EmployeeWebApplication.Business_Logic
{
    public class AnnualDeductionRate : IAnnualDeductionRate
    {
        public decimal Get(PersonType personType)
        {
            switch (personType)
            {
                case Entites.PersonType.Employee:
                    return Constants.EMPLOYEE_DEDUCTION_PER_YEAR;
                case Entites.PersonType.Spouse:
                case Entites.PersonType.Child:
                    return Constants.DEPENDENT_DEDUCTION_PER_YEAR;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
