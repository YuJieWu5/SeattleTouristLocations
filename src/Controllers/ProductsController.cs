using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{   
    /// <summary>
    /// The controller class for the website
    /// </summary>
    
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// Data Middletier
        
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Aquires and returns an IEnumerable of products in our data store 
        /// </summary>
        /// <returns> IEnumerable </returns>
        
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetProducts();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="request"></param>
        /// <returns> ActionResult </returns>
        
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }

        /// <summary>
        /// The ratingrequest class for the website
        /// </summary>
        public class RatingRequest
        {
            // The product id of the product
            public string ProductId { get; set; }
            
            // The curent rating of the product
            public int Rating { get; set; }
        }

    }

}