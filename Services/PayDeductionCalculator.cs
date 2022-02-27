using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebApplication.Models;
using EmployeeWebApplication.Business_Logic.Interfaces;
using EmployeeWebApplication.Helpers;
using EmployeeWebApplication.Entites;

namespace EmployeeWebApplication.Services
{
    public class PayDeductionCalculator : IPayDeductionCalculator
    {
        private readonly IDeductionCalculator _payDeductionCalculator;
        public PayDeductionCalculator(IDeductionCalculator payDeductionCalculator)
        {
            _payDeductionCalculator = payDeductionCalculator;
        }

        public DeductionResults CalculateDeductions(Employee employee)
        {
            List<Entites.Person> persons = Converters.ConvertEmployeeToPersonList(employee);
            decimal employeeDedudctionPerPayCheck = _payDeductionCalculator.CalculateDeductionPerPaycheck(persons.Where(p => p.Type == Entites.PersonType.Employee).ToList(), employee.NumberOfPaychecksPerYear);
            decimal dependentsDeductionPerPayCheck = _payDeductionCalculator.CalculateDeductionPerPaycheck(persons.Where(p => p.Type != Entites.PersonType.Employee).ToList(), employee.NumberOfPaychecksPerYear);
            decimal totalDeductionPerPayCheck = _payDeductionCalculator.CalculateDeductionPerPaycheck(persons, employee.NumberOfPaychecksPerYear);
            decimal employeeDeductionPerYear = _payDeductionCalculator.CalculateDeductionPerAnnum(persons.Where(p => p.Type == Entites.PersonType.Employee).ToList());
            decimal dependentDeductionPerYear = _payDeductionCalculator.CalculateDeductionPerAnnum(persons.Where(p => p.Type != Entites.PersonType.Employee).ToList());
            decimal totalDeductionPerYear = _payDeductionCalculator.CalculateDeductionPerAnnum(persons);
            decimal employeePaycheckAfterDeductions = (employee.YearlySalary / (decimal)employee.NumberOfPaychecksPerYear) - totalDeductionPerPayCheck;
            decimal employeeYearlyPayAfterDeductions = employee.YearlySalary - totalDeductionPerYear;

            return new DeductionResults()
            {
                EmployeeDeductionPerPayCheck = employeeDedudctionPerPayCheck,
                DependentsDeductionPerPayCheck = dependentsDeductionPerPayCheck,
                TotalDeductionPerPayCheck = totalDeductionPerPayCheck,
                EmployeeDeductionPerYear = employeeDeductionPerYear,
                DependentsDeductionPerYear = dependentDeductionPerYear,
                TotalDeductionPerYear = totalDeductionPerYear,
                EmployeePaycheckAfterDeductions = employeePaycheckAfterDeductions,
                EmployeeYearlyPayAfterDeductions = employeeYearlyPayAfterDeductions
            };
        }
    }
}
