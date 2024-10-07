using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContosoCrafts.WebSite.Enums
{
    public enum LocationCategoryEnum
    {
        /// Types of location categories
        [Display(Name = "Please Select")] Undefined = 0,
        [Display(Name = "Famous Tourist Attractions")] Tourists = 1,
        [Display(Name = "Famous Restaurants")] Restaurants = 2,
        [Display(Name = "Famous Outdoor Places")] Outdoor = 3,
        [Display(Name = "Famous Tea Locations")] Tea = 4,
        [Display(Name = "Other Famous Locations")] Others = 5
    }

}