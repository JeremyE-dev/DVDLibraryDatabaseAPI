//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rating
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rating()
        {
            this.Dvds = new HashSet<Dvd>();
        }
    
        public int RatingId { get; set; }
        public string RatingName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dvd> Dvds { get; set; }
    }
}
