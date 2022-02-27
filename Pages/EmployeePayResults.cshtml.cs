using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeWebApplication.Helpers;
using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.Pages
{
    public class EmployeePayResultsModel : PageModel
    {
        public DeductionResults CalcuationResults { get; set; }

        public IActionResult OnGet()
        {
            CalcuationResults = TempData.Get<DeductionResults>(Constants.RESULTS_TEMP_DATA_KEY);

            if (CalcuationResults == null)
            {
                return RedirectToPage(Constants.INDEX_PAGE);
            }

            // Reset the calculation results in temp data to ensure that they are available if the user refreshes the page
            TempData.Set(Constants.RESULTS_TEMP_DATA_KEY, CalcuationResults);

            return Page();
        }
    }
}
