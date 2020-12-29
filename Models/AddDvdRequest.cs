using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public class AddDvdRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Rating { get; set; }

        //add additional fields for the request as needed


    }
}