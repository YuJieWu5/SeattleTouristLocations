using System.Linq;

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

using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace UnitTests.Pages.Index
{
    /// <summary>
    /// Testing class for the Index.cshtml.cs file.
    /// </summary>
    public class IndexTests
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

        public static IndexModel pageModel;

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

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new IndexModel(MockLoggerDirect, productService)
            {
            };
        }

        #endregion TestSetup


        #region OnGet

        /// <summary>
        /// Testing the OnGet function in Index.cshtml.cs file.
        /// If the SearchProduct and SearchCategory are valid, it should return products
        /// </summary>
        [Test]
        public void OnGet_Valid_SearchProduct_and_SearchCategory_Should_Return_Products()
        {
            // Arrange
            pageModel.SearchProduct = "The Fremont Troll";
            pageModel.SearchCategory = (int)LocationCategoryEnum.Tea;

            //Act
            var  result = pageModel.OnGet(pageModel.SearchProduct, pageModel.SearchCategory);
            IEnumerable<ProductModel> products = TestHelper.ProductService.GetProducts().Where(
                m => m.Title.ToLower().StartsWith(pageModel.SearchProduct.ToLower()) && (int)m.LocationCategory == pageModel.SearchCategory
            );

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(products.ToString(), pageModel.Products.ToString());
        }

        /// <summary>
        /// Testing the OnGet function in Index.cshtml.cs file.
        /// If the SearchProduct valid, it should return products
        /// </summary>
        [Test]
        public void OnGet_Valid_SearchProduct_Should_Return_Products()
        {
            // Arrange
            pageModel.SearchProduct = "The Fremont Troll";
            pageModel.SearchCategory = 0;

            //Act
            var result = pageModel.OnGet(pageModel.SearchProduct, pageModel.SearchCategory);
            IEnumerable<ProductModel> products = TestHelper.ProductService.GetProducts().Where(
                m => m.Title.ToLower().StartsWith(pageModel.SearchProduct.ToLower())
            );

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(products.ToString(), pageModel.Products.ToString());
        }

        /// <summary>
        /// Testing the OnGet function in Index.cshtml.cs file.
        /// If the SearchCategory valid, it should return products
        /// </summary>
        [Test]
        public void OnGet_Valid_SearchCategory_Should_Return_Products()
        {
            // Arrange
            pageModel.SearchProduct = null;
            pageModel.SearchCategory = (int)LocationCategoryEnum.Outdoor;

            //Act
            var result = pageModel.OnGet(pageModel.SearchProduct, pageModel.SearchCategory);
            IEnumerable<ProductModel> products = TestHelper.ProductService.GetProducts().Where(
                m => (int)m.LocationCategory == pageModel.SearchCategory
            );

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(products.ToString(), pageModel.Products.ToString());
        }

        /// <summary>
        /// Testing the OnGet function in Index.cshtml.cs file.
        /// If two filter are null, it should return all products
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.SearchProduct = null;
            pageModel.SearchCategory = 0;

            //Act
            var result = pageModel.OnGet(pageModel.SearchProduct, pageModel.SearchCategory);
            IEnumerable<ProductModel> products = TestHelper.ProductService.GetProducts();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(products.ToString(), pageModel.Products.ToString());
        }
        #endregion OnGet
    }
}