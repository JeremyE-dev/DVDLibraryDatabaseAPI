using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public class UpdateDvdRequest
    {
        [Required]
        public int DvdId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Rating { get; set; }

    }
}