using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// The class connected to the Index page, provides back end triggered functionality
    /// </summary>

    public class IndexModel : PageModel
    {
        //the logger of the class
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// constructor of the class, sets up the logger and the data service connection
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        // data middletier
        public JsonFileProductService ProductService { get; }

        //The list of products to be displayed to the user

        public IEnumerable<ProductModel> Products { get; private set; }

        //To store the search bar input
        [BindProperty(SupportsGet = true)]
        public string SearchProduct { get; set; }

        //To store the selected category
        [BindProperty(SupportsGet = true)]
        public int SearchCategory { get; set; }

        /// <summary>
        /// To get Data
        /// </summary>
        /// <param name="SearchProduct"></param>
        /// <returns></returns>
        public IActionResult OnGet(string SearchProduct, int SearchCategory)
        {
            //Checks if all filter are null
            if (string.IsNullOrEmpty(SearchProduct) && SearchCategory == 0)
            {
                //Assigns all the products existing to Products
                Products = ProductService.GetProducts();
            }
            //Checks if only search bar input has data
            else if (!string.IsNullOrEmpty(SearchProduct) && SearchCategory == 0)
            {
                //Assigns the searched product to Products
                Products = ProductService.GetProducts().Where(m => m.Title.ToLower().StartsWith(SearchProduct.ToLower()));
            }

            //Checks if only search category has data
            else if (string.IsNullOrEmpty(SearchProduct) && SearchCategory != 0)
            {
                // Assigns the searched category to Products
                Products = ProductService.GetProducts().Where(m => (int)m.LocationCategory == SearchCategory);
            }

            //Checks if two filters have data
            else if (!string.IsNullOrEmpty(SearchProduct) && SearchCategory != 0)
            {
                // Assigns the searched category and product to Products
                Products = ProductService.GetProducts().Where(m => m.Title.ToLower().StartsWith(SearchProduct.ToLower()) && (int)m.LocationCategory == SearchCategory);
            }

            return Page();
        }
    }
}