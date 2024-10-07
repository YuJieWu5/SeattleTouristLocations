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
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Pages.Product.Delete
{

    /// <summary>
    /// Testing class for the Delete.cshtml.cs file.
    /// </summary>
	public class DeleteTests
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

        public static DeleteModel pageModel;

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

            var MockLoggerDirect = Mock.Of<ILogger<DeleteModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new DeleteModel(productService)
            {
            };
        }
        #endregion TestSetup

        /// <summary>
        /// Testing the OnGet function in Delete.cshtml.cs file.
        /// If the Id valid, it should return products
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("2");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("2", pageModel.Product.Id);
        }
        #endregion OnGet

        /// <summary>
        /// Testing the OnPost function in Delete.cshtml.cs file.
        /// If the Id valid, it should redirect to Index page without deleted product
        /// </summary>
        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Return_Index_Page()
        {
            //Copy original data
            IEnumerable<ProductModel> Original_Product = TestHelper.ProductService.GetProducts();

            // Arrange
            pageModel.Product = new ProductModel { Id = "1" };

            // Act
            pageModel.OnPost();
            var productListAfterDeletion = TestHelper.ProductService.GetProducts();

            //Reset the Data
            TestHelper.ProductService.SaveData(Original_Product);

            //find the data
            var deletedProduct = productListAfterDeletion.FirstOrDefault(m => m.Id.Equals("1"));

            //Assert - this will check whether the data is not existed 
            Assert.AreEqual(null, deletedProduct);
        }

        /// <summary>
        /// Testing the OnPost function in Delete.cshtml.cs file.
        /// If the Id invalid, it should redirect to Index page, no data will be changed
        /// </summary>
        [Test]
        public void OnPost_invalid_Should_Return_Index_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel();

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.AreEqual("../Error", redirectToPageResult.PageName);

        }
        #endregion OnPost
    }

}