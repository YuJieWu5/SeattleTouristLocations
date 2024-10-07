using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Pages.Product.AddRating
{
    /// <summary>
    /// Testing class for the JsonFileProductServices.cs file.
    /// </summary>
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating

        /// <summary>
        /// Tests for the case for when the product ID is null
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Provided Test, tests for the case when the product id string is empty
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests for the case of a prouct Id that does not match the 
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Not_Found_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("missing-Id", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests the filtering functionality which does not allow for ratings below 1
        /// </summary>
        [Test]
        public void AddRating_InValid_Rating_Too_low_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("1", -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests the filtering functionality which does not allow for ratings larger than 5
        /// </summary>
        [Test]
        public void AddRating_InValid_Rating_Too_High_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("2", 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests for the case of when the product id matches with a productmodel who has null ratings
        /// </summary>
        //(NOTE: the 3 in the second parameter spot is arbitrary)
        [Test]
        public void AddRating_Valid_Rating_Is_Null_Should_Return_True()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("4", 3);

            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion AddRating

        #region CreateData

        /// <summary>
        /// This method tests whether the creation of the new product was successful
        /// </summary>
        [Test]
        public void CreateData_Valid_ProductModel_Should_Return_True()
        {
            // Arrange
            var product = new ProductModel
            {
                Title = "New Place",
                Description = "Hello New Place!"
            };

            // Act
            var result = TestHelper.ProductService.CreateData(product);

            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion CreateData

        #region UpdateData

        ///<summary>
        ///This method is to check for valid product ID.
        /// </summary>
        [Test]
        public void UpdateData_Valid_ProductModel_ID_Is__Not_Null_Should_Return_True()
        {
            //Copy original data
            IEnumerable<ProductModel> Original_Product = TestHelper.ProductService.GetProducts();

            //Arrange
            var product = new ProductModel
            {
                Id = "18",
                Location = null,
                Image = "https://i0.wp.com/aroundtheworldwithkids.net/wp-content/uploads/2022/02/sn-intro.jpg?fit=1200%2C1200\u0026ssl=1",
                Title = "The Space Needle",
                Description = "The Space Needle is an observation tower in Seattle, Washington, United States. Considered to be an icon of the city, it has been designated a Seattle landmark. Located in the Lower Queen Anne neighborhood, it was built in the Seattle Center for the 1962 World\u0027s Fair, which drew over 2.3 million visitors.",
                Ratings = null
            };

            //Act
            var result = TestHelper.ProductService.UpdateData(product);

            //Assert
            Assert.AreEqual(true, result);

            //Reset the data
            TestHelper.ProductService.SaveData(Original_Product);
        }

        ///<summary>
        ///This method is to checks for an invalid product ID.
        /// </summary>
        [Test]
        public void UpdateData_InValid_ProductModel_Location_Is_Null_Should_Return_False()
        {
            //Arrange
            var product = new ProductModel
            {
                Id = null,
                Location = null,
                Image = "https://i0.wp.com/aroundtheworldwithkids.net/wp-content/uploads/2022/02/sn-intro.jpg?fit=1200%2C1200\u0026ssl=1",
                Title = "The Space Needle",
                Description = "The Space Needle is an observation tower in Seattle, Washington, United States. Considered to be an icon of the city, it has been designated a Seattle landmark. Located in the Lower Queen Anne neighborhood, it was built in the Seattle Center for the 1962 World\u0027s Fair, which drew over 2.3 million visitors.",
                Ratings = null
            };

            //Act
            var result = TestHelper.ProductService.UpdateData(product);

            //Assert
            Assert.AreEqual(false, result);
        }

        #endregion UpdateData

        #region DeleteData

        ///<summary>
        ///This method is to check for valid product data, if it is valid it will return true.
        ///</summary>
        [Test]
        public void DeleteData_Valid_Product_Should_Return_True()
        {
            //Copy original data
            IEnumerable<ProductModel> Original_Product = TestHelper.ProductService.GetProducts();

            //Arrange- creating a object of type product model and then passing to delete data function
            var product = new ProductModel
            {
                Id = "1",
                Location = "400 Broad St, Seattle, WA 98109"
            };

            //Act
            TestHelper.ProductService.DeleteData(product);
            var productListAfterDeletion = TestHelper.ProductService.GetProducts();

            //Reset the data
            TestHelper.ProductService.SaveData(Original_Product);

            //find the data
            var deletedProduct = productListAfterDeletion.FirstOrDefault(m => m.Id.Equals("1"));

            //Assert - this will check whether the data is not existed 
            Assert.AreEqual(null, deletedProduct);
        }

        ///<summary>
        ///This method is to check for invalid product data, if it is invalid it will return false.
        /// </summary>
        [Test]
        public void DeleteData_InValid_ProductModel_Invalid_Product_Id_Should_Return_False()
        {
            //create a new object of type product model and pass a null value for id
            var product = new ProductModel
            {
                Id = null ,
                Location = "400 Broad St, Seattle, WA 98109",
                Image = "https://i0.wp.com/aroundtheworldwithkids.net/wp-content/uploads/2022/02/sn-intro.jpg?fit=1200%2C1200\u0026ssl=1",
                Title = "The Space Needle",
                Description = "The Space Needle is an observation tower in Seattle, Washington, United States. Considered to be an icon of the city, it has been designated a Seattle landmark. Located in the Lower Queen Anne neighborhood, it was built in the Seattle Center for the 1962 World\u0027s Fair, which drew over 2.3 million visitors.",
                Ratings = null
            };
            
            //Act - Pass the product to delete data functionality and this will return boolean value
            var result = TestHelper.ProductService.DeleteData(product);

            //Assert - this will check whether the boolean value matches
            Assert.AreEqual(false,result);
        }

        #endregion DeleteData
    }
}