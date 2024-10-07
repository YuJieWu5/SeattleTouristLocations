using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Product
{

    /// <summary>
    /// The class connected to the Delete page,
    /// provides OnPost function that trigger DeleteData functionality on JsonFileProductService.cs
    /// </summary>
	public class DeleteModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        // The data to show
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Get request, triggered when the page is first requested
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
                return RedirectToPage("../Error");
            return Page();
        }

        /// <summary>
        /// Post request, triggered after delete button be clicked
        /// It will send the Product to DeleteData functionality on JsonFileProductService.cs
        /// When the DeleteData completed, it will redirect to Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (Product.Id == null)
            {
                //return error page
                return RedirectToPage("../Error");
            }

            //if the product id exist in json file
            else if (ProductService.DeleteData(Product))
            {
                return RedirectToPage("./Index");
            }

            //if the product id is not exist in json file 
            return RedirectToPage("../Error");  
        }

    }

}