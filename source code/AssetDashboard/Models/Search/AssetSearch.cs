using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarTrack.Dashboard.Models.Search
{
    public class AssetSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TagId { get; set; }
        public int Status { get; set; }
        public string CreateDate { get; set; }
    }
}