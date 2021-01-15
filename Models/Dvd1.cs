using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public partial class Dvd
    {
        public int? releaseYear { get; set; }
        public string director { get; set; }

        public string rating { get; set; }

        public string title { get; set; }
        public string notes { get; set; }


        //public int? ReleaseYearName { get; set; }
        //public string DirectorName { get; set; }

        //public string RatingName { get; set; }




    }
}