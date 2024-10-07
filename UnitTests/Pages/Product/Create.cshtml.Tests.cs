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
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages.Location;
namespace UnitTests.Pages.Product
{
    /// <summary>
    /// Testing class for the Create.cshtml.cs file.
    /// </summary>
    public class CreateTests
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
        public static CreateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                ///RequestServices = serviceProviderMock.Object,
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

            var MockLoggerDirect = Mock.Of<ILogger<CreateModel>>();

            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new CreateModel(productService)
            {
            };

        }

        #endregion TestSetup

        [Test]
        public void OnGet_Valid_Should_Initialize_ProductModel()
        {
            
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.IsNotNull(pageModel.Product);
        }

        #region OnPostCreate

        [Test]
        public void OnPostCreate_Invalid_Model_Should_Return_PageResult()
        {
            
            // Arrange
            pageModel.ModelState.AddModelError("Property", "Error message");

            // Act
            var result = pageModel.OnPostCreate();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
        }

        [Test]
        public void OnPostCreate_Valid_Model_Should_Return_RedirectToPageResult()
        {
            
            // Arrange
            pageModel.Product = new ProductModel();

            // Act
            var result = pageModel.OnPostCreate();

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
        }

        #endregion OnPostCreate
    }

}