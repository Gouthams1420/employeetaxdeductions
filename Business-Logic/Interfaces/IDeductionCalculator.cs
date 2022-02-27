using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebApplication.Entites;

namespace EmployeeWebApplication.Business_Logic.Interfaces
{
    public interface IDeductionCalculator
    {
        /// <summary>
        /// Returns the benefits deduction amount per paycheck for a given family.
        /// </summary>
        /// <param name="persons">Persons within an employee's family.</param>
        /// <param name="numberOfPaychecksPerYear">Number of paychecks the employee recieves per year</param>
        /// <returns>Benefits deduction amount per paycheck for the employee</returns>
        decimal CalculateDeductionPerPaycheck(List<Person> persons, int numberOfPaychecksPerYear);

        /// <summary>
        /// Returns the benefits deduction amount per year for a given family.
        /// </summary>
        /// <param name="persons">Persons within an employee's family</param>
        /// <returns>Benefits deduction amount per year for the employee</returns>
        decimal CalculateDeductionPerAnnum(List<Person> persons);

        /// <summary>
        /// Returns the benefits deduction amount for the person. This method takes into account any discounts
        /// that the person might be eligible for.
        /// </summary>
        /// <param name="person">Person whose deduction amount should be calculated</param>
        /// <returns>Benefits deduction amount with any discount that the person is eligible for</returns>
        decimal CalculateDeductionWithDiscount(Person person);
    }
}
