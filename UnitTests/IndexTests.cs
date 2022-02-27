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
using System.Diagnostics.CodeAnalysis;
using TestToolsToXunitProxy;

namespace EmployeeWebApplication.UnitTests
{
    [TestClass]
    public class IndexTests
    {
        [TestMethod]
        public void verify_dependentnumberlist_setup_in_constructor()
        {
            // Arrange
            var objectUnderTest = new IndexModel();

            // Act

            // Assert
            Assert.IsNotNull(objectUnderTest.DependentNumberList);
            Assert.IsTrue(objectUnderTest.DependentNumberList.Count > 0);
        }

        [TestMethod]
        public void verify_page_returned_on_invalid_model_state()
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
            var objectUnderTest = new IndexModel()
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
            objectUnderTest.ModelState.AddModelError("NumberOfDependents", "NumberOfDependents is invalid.");

            // Act
            var result = objectUnderTest.OnPost();

            // Assert
            Assert.ReferenceEquals(result, typeof(PageResult));
        }

        [TestMethod]
        public void verify_redirect_to_employee_information_page()
        {
            // Arrange
            var objectUnderTest = new IndexModel();
            objectUnderTest.NumberOfDependents = 3;

            // Act
            var result = objectUnderTest.OnPost();

            // Assert
            Assert.ReferenceEquals(result, typeof(RedirectToPageResult));
            Assert.IsTrue(((RedirectToPageResult)result).PageName == EmployeeWebApplication.Helpers.Constants.EMPLOYEE_INFORMATION_PAGE);
            Assert.IsTrue(((RedirectToPageResult)result).RouteValues[nameof(objectUnderTest.NumberOfDependents)].ToString() == "3");
        }
    }
}
