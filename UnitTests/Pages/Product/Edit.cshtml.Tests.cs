using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;


namespace UnitTests.Pages.Product.Edit
{
    /// <summary>
    /// Testing class for the Edit.cshtml.cs file.
    /// </summary>
    public class EditTests
    {
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static EditModel pageModel;

        /// <summary>
        /// Method to set up everything required for the testing routine below
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<EditModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new EditModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the valid case of the OnGet method
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("10");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("10", pageModel.Product.Id);
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Tests for the invlaid case of the Onpost method
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests for the valid state of the Onpost method
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.OnGet("5");
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            var originalTitle = pageModel.Product.Title;

            // change Value to Microsoft  and update
            pageModel.Product.Title = "The Gum Wall!!!";
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert to see that post succeeeded
            Assert.AreEqual(true, pageModel.ModelState.IsValid);

            // Read it to see if it changed
            pageModel.OnGet("5");

            // Assertions to verify
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("The Gum Wall!!!", pageModel.Product.Title);

            // Reset it back
            pageModel.Product.Title = originalTitle;
            result = pageModel.OnPost() as RedirectToPageResult;
            // Assert to see that post succeeeded
            Assert.AreEqual(true, pageModel.ModelState.IsValid);

            // Reset 
            pageModel.ModelState.Clear();
        }

        #endregion OnPost
    }
}