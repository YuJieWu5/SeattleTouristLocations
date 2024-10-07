using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// The class connected to the Edit page, provides back end triggered functionality
    /// </summary>
	public class EditModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public EditModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show
        [BindProperty]
        public ProductModel Product { get; set; }

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
        /// Post request, triggered after the save button on the page is clicked
        /// </summary>
        /// <returns> IActionResult </returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("../Error");
            }
            else if (ProductService.UpdateData(Product))
            {
                return RedirectToPage("./Index");

            }
            return RedirectToPage("./Index");

        }
    }
}