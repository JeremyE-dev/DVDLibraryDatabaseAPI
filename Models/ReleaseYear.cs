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
    
    public partial class ReleaseYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReleaseYear()
        {
            this.Dvds = new HashSet<Dvd>();
        }
    
        public int ReleaseYearId { get; set; }
        public int ReleaseYear1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dvd> Dvds { get; set; }
    }
}
