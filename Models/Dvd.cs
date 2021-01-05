using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public class Dvd
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }

        public string DirectorName { get; set; }
        public string RatingName { get; set; }
        public string Notes { get; set; }

    }
}