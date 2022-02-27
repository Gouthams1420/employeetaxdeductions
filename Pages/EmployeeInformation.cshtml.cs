using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeWebApplication.Services;
using EmployeeWebApplication.Models;
using EmployeeWebApplication.Helpers;

namespace EmployeeWebApplication.Pages
{
    public class EmployeeInformationModel : PageModel
    {
        private readonly IPayDeductionCalculator _payDeductionCalculator;

        [BindProperty]
        public Employee Employee { get; set; }

        public List<SelectListItem> DependentTypeList { get; } = new List<SelectListItem>();

        public EmployeeInformationModel(IPayDeductionCalculator payDeductionCalculator)
        {
            _payDeductionCalculator = payDeductionCalculator;

            PopulateDependentTypeList();
        }

        #region HTTP Actions
        public IActionResult OnGet(int? numberOfDependents)
        {
            if (numberOfDependents == null)
            {
                return RedirectToPage(Constants.INDEX_PAGE);
            }

            Employee = CreateEmployeeViewModel(numberOfDependents);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Store the results in temp data so that the results page can retrieve them
            TempData.Set(Constants.RESULTS_TEMP_DATA_KEY, _payDeductionCalculator.CalculateDeductions(Employee));

            return RedirectToPage(Constants.RESULTS_PAGE);
        }
        #endregion

        #region Helper Methods
        private void PopulateDependentTypeList()
        {
            foreach (var value in Enum.GetValues(typeof(DependentType)))
            {
                DependentTypeList.Add(new SelectListItem(value.ToString(), value.ToString()));
            }
        }

        private Employee CreateEmployeeViewModel(int? numberOfDependents)
        {
            var employee = new Employee();

            employee.YearlySalary = Constants.SALARY_PER_PAYCHECK * Constants.NUMBER_OF_PAYCHECKS_PER_YEAR;
            employee.NumberOfPaychecksPerYear = Constants.NUMBER_OF_PAYCHECKS_PER_YEAR;

            for (int i = 0; i < numberOfDependents; i++)
            {
                employee.Dependents.Add(new EmployeeDependent() { Type = (i == 0 ? DependentType.Spouse : DependentType.Child) });
            }

            return employee;
        }
        #endregion

    }
}
