//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarTrack.Dashboard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reader()
        {
            this.Antinnas = new HashSet<Antinna>();
        }
    
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int AntinnaCount { get; set; }
        public string Detail { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antinna> Antinnas { get; set; }
        public virtual Site Site { get; set; }
    }
}