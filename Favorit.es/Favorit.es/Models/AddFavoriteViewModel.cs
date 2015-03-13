using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favorit.es.Models
{
    public class AddFavoriteViewModel
    {
        //this will contain the information for passing our favorite data from the view to the controller.  
        //It will also serve as the object to store our list of favorites.
        public string id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }
}