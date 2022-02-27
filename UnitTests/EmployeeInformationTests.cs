using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeWebApplication.Pages;
using EmployeeWebApplication.Services;
using EmployeeWebApplication.Models;
using TestToolsToXunitProxy;

namespace EmployeeWebApplication.UnitTests
{
    [TestClass]
    public class EmployeeInformationTests
    {
        [TestMethod]
        public void verify_dependenttypelist_setup_in_constructor()
        {
            // Arrange            
            var objectUnderTest = new EmployeeInformationModel(A.Fake<IPayDeductionCalculator>());

            // Act

            // Assert
            Assert.IsNotNull(objectUnderTest.DependentTypeList);
            Assert.IsTrue(objectUnderTest.DependentTypeList.Count > 0);
        }

        [TestMethod]
        public void verify_redirect_to_index_page_if_no_numberofdependents_parameter_passed_to_onget()
        {
            // Arrange            
            var objectUnderTest = new EmployeeInformationModel(A.Fake<IPayDeductionCalculator>());

            // Act
            var result = objectUnderTest.OnGet(null);

            // Assert
            Assert.ReferenceEquals(result, typeof(RedirectToPageResult));
            Assert.IsTrue(((RedirectToPageResult)result).PageName == EmployeeWebApplication.Helpers.Constants.INDEX_PAGE);
        }

        [TestMethod]
        public void verify_page_shown_if_numberofdependents_passed_to_onget()
        {
            // Arrange            
            var objectUnderTest = new EmployeeInformationModel(A.Fake<IPayDeductionCalculator>());

            // Act
            var result = objectUnderTest.OnGet(3);

            // Assert
            Assert.ReferenceEquals(result, typeof(PageResult));
            Assert.IsNotNull(objectUnderTest.Employee.Dependents.Count);
            Assert.IsTrue(objectUnderTest.Employee.Dependents.Count == 3);
        }

        [TestMethod]
        public void verify_page_returned_on_invalid_model_state_onpost()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, A.Fake<ITempDataProvider>());
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };
            var objectUnderTest = new EmployeeInformationModel(A.Fake<IPayDeductionCalculator>())
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
            objectUnderTest.ModelState.AddModelError("Employee.Name", "Name is required.");

            // Act
            var result = objectUnderTest.OnPost();

            // Assert
            Assert.ReferenceEquals(result, typeof(PageResult));
        }

        [TestMethod]
        public void verify_calculation_and_redirect_onpost()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, A.Fake<ITempDataProvider>());
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };
            IPayDeductionCalculator payDeductionCalculator = A.Fake<IPayDeductionCalculator>();
            var objectUnderTest = new EmployeeInformationModel(payDeductionCalculator)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };

            // Act
            var result = objectUnderTest.OnPost();

            // Assert
            Assert.ReferenceEquals(result, typeof(RedirectToPageResult));
            Assert.IsTrue(((RedirectToPageResult)result).PageName == EmployeeWebApplication.Helpers.Constants.RESULTS_PAGE);
            A.CallTo(() => payDeductionCalculator.CalculateDeductions(A<Employee>._)).MustHaveHappened();
        }
    }
}
