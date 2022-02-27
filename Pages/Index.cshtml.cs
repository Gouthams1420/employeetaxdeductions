using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
//using EmployeeWebApplication.Pages.Code;
using EmployeeWebApplication.Services;
using EmployeeWebApplication.Models;
using EmployeeWebApplication.Business_Logic;
using EmployeeWebApplication.Helpers;

namespace EmployeeWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int NumberOfDependents { get; set; }

        public List<SelectListItem> DependentNumberList { get; } = new List<SelectListItem>();

        public IndexModel()
        {
            PopulateDependentNumberList();
        }

        #region HTTP Actions
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage(Helpers.Constants.EMPLOYEE_INFORMATION_PAGE, new { NumberOfDependents = NumberOfDependents });

        }
        #endregion

        #region Helper Methods
        private void PopulateDependentNumberList()
        {
            for (var i = 0; i <= 10; i++)
            {
                DependentNumberList.Add(new SelectListItem($"{(i == 0 ? "No" : i.ToString())} Dependent{(i == 1 ? "" : "s")}", i.ToString()));
            }
        }
        #endregion
    }
}