using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// The class connected to the privacy page, provides back end triggered functionality
    /// </summary>
    public class PrivacyModel : PageModel
    {

        //The logger for the page
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Constructor, sets up the logger of the class
        /// </summary>
        /// <param name="logger"></param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Onget method for when the page is called, not currently being used
        /// </summary>
        public void OnGet()
        {
        }

    }
}
