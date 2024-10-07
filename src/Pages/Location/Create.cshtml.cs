using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ContosoCrafts.WebSite.Pages.Location
{
    
    /// <summary>
    /// The class connected to the Create page, provides back end triggered functionality
    /// </summary>
	public class CreateModel : PageModel
    {

        /// <summary>
        /// Defualt Constructor
        /// </summary>
        /// <param name="productService"></param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Data middletier
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// the product connected to the create page
        /// </summary>
        [BindProperty]
        public ProductModel  Product { get; set; }

        /// <summary>
        /// get Request, triggered when the page is first requested
        /// </summary>
        public void OnGet()
        {
            Product = new ProductModel();
        }

        /// <summary>
        /// Post Request, triggered after the save button on the create page is clicked
        /// </summary>
        /// <returns> IActionResult </returns>
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.CreateData(Product);

            return RedirectToPage("./Index");
        }

    }

}