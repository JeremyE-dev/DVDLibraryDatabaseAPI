using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public partial class Dvd
    {
        public int? ReleaseYearName { get; set; }
        public string DirectorName { get; set; }

        public string RatingName { get; set; }
    }
}