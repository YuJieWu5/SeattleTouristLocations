using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// The class connected to the Error page, provides back end triggered functionality
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        //RequestId, request Id to be displayed to the user on the error page
        public string RequestId { get; set; }

        //The ShowrequestId of the read page, (not currently used/implmented)
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        //The logger of the read page (not currently used/implemented)
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// The logger of the Errormodel class, (not currently used/implemented)
        /// </summary>
        /// <param name="logger"></param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// onget method, triggered when the page is first requested
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}