using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.Services
{
    public interface IPayDeductionCalculator
    {
        /// <summary>
        /// This method takes an employee and returns the deductions calculated for that employee. 
        /// </summary>
        /// <param name="employee">Employee with this dependents and basic salary informaton needed to calculate deductions.</param>
        /// <returns></returns>
        DeductionResults CalculateDeductions(Employee employee);
    }
}
