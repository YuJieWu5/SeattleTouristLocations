using ContosoCrafts.WebSite.Services;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using UnitTests;
using ContosoCrafts.WebSite.Components;
using NUnit.Framework;
using System.Linq;
using AngleSharp.Dom;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace BlazorTests
{
    /// <summary>
    /// This class provides the testing for the ProductList.razor file
    /// </summary>
    public class BlazorTests : Bunit.TestContext
    {
        
        #region productlist

        /// <summary>
        /// Tests the ProductList function in productList.razor file
        /// </summary>
        
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Get the Cards Returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("The Space Needle"));
        }

        #endregion productlist

        #region selectproduct

        /// <summary>
        /// Tests the SelectProduct function in productList.razor file
        /// </summary>
        
        [Test]
        public void SelectProduct_Valid_ID_1_Should_Return_Content()
        {
            // Arrange
            //Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_1";

            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.Products, TestHelper.ProductService.GetProducts()));

            // Find the Buttons (more info)
            var buttonlist = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonlist.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("The Space Needle is an observation tower in Seattle"));

        }

        #endregion selectproduct

        #region SubmitRating

        /// <summary>
        /// Tests the submit rating function in ProductList.razor file
        /// </summary>

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            /*
            This test tests that the SubmitRating will change the vote as well as the Star checked
            Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear that was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the count and star check status
             */

            // Arrange
            var id = "MoreInfoButton_19";

            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.Products, TestHelper.ProductService.GetProducts()));

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the click action
            var buttonMarkup = page.Markup;

            // Get the star Buttons
            var starButtonList = page.FindAll("span");

            // Get the vote count
            // The list should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the star Buttons
            starButtonList = page.FindAll("span");

            // Get the vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Assert

            // Confirm that the record had 1 vote to start, and 2 votes to end
            Assert.IsTrue(preVoteCountString.Contains("Be the first to vote!"));
            Assert.IsTrue(postVoteCountString.Contains("1 Vote"));
            Assert.IsFalse(preVoteCountString.Equals(postVoteCountString));
        }

        /// <summary>
        /// Tests the submit rating function in ProductList.razor file
        /// </summary>

        [Test]
        public void SubmitRating_Valid_ID_Click_stared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            var id = "MoreInfoButton_19";

            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.Products, TestHelper.ProductService.GetProducts()));

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the click action
            var buttonMarkup = page.Markup;

            // Get the star Buttons
            var starButtonList = page.FindAll("span");

            // Get the vote count
            // The list should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the star Buttons
            starButtonList = page.FindAll("span");

            // Get the vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.IsTrue(preVoteCountString.Contains("1 Vote"));
            Assert.IsTrue(postVoteCountString.Contains("2 Votes"));
            Assert.IsFalse(preVoteCountString.Equals(postVoteCountString));

        }

        #endregion SubmitRating

    }

}