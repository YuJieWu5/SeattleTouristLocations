using ContosoCrafts.WebSite.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// The product model class, represents each of the individual items on the webapge
    /// </summary>
    
    public class ProductModel
    {
        // The Id string
        public string Id { get; set; }

        // The location string
        [Required(ErrorMessage = "Please add a location (no special characters allowed")]
        [RegularExpression(@"^[0-9a-zA-Z.\-,\s]+$", ErrorMessage = "No special characters allowed in the location")]
        public string Location { get; set; }

        // The Image string
        [JsonPropertyName("img")]
        [Required(ErrorMessage = "Please add an img url")]
        [RegularExpression(@"\bhttps?://\S*\.(jpg|jpeg|png)\b", ErrorMessage = "Please enter a valid img url")]
        public string Image { get; set; }

        // The title string
        [Required(ErrorMessage = "Please add a title")]
        [RegularExpression(@"^[0-9a-zA-Z.,\s]*[^\s]+[0-9a-zA-Z.,\s]*$", ErrorMessage = "Please insert valid string")]
        public string Title { get; set; }

        // The description String
        [Required(ErrorMessage = "Please add a description")]
        [RegularExpression(@"^[0-9a-zA-Z.,\s]*[^\s]+[0-9a-zA-Z.,\s]*$", ErrorMessage = "Please insert valid string")]
        public string Description { get; set; }

        // The ratings array
        public int[] Ratings { get; set; }

        // The contact number string 
        [Required(ErrorMessage = "Please add an embedded link ")]
        [RegularExpression("^[0-9]*$", ErrorMessage ="Please insert only numbers.")]
        public string ContactNumber { get; set; }

        // The embedded link for the map view
        [Required(ErrorMessage = "Please add an embeded link from google maps")]
        [RegularExpression("^http.*sus$", ErrorMessage = "Please add a proper embedded link")]
        public string E_link { get; set; }

        // The LocationCategory Enum
        public LocationCategoryEnum LocationCategory { get; set; } = LocationCategoryEnum.Undefined;
        
        /// <summary>
        /// Override of the standard Tostring method, serializes data instead of creating a standard string
        /// </summary>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

    }

}