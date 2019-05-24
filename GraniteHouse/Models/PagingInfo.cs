using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class PagingInfo
    {
        // propertiesfor the pagination tag helper
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        // totalPages takes into account the last page that might not have the complete ItemsPerPage items
        public int totalPage => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        //// The above "TotalPages => ..." is shortened syntax for
        //public int TotalPages
        //{ get
        //    {
        //        return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        //    }
        //}

        // this will be used to build url
        public string urlParam { get; set; }
    }
}
