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
    
    public partial class SubAsset
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string TagId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Asset Asset { get; set; }
    }
}