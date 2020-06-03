using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarTrack.Dashboard.Models
{
    public class AssetListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AID { get; set; }
        public string TagId { get; set; }
        public AssetStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Location { get; set; }
    }
    public class AssetResult
    {
        public AssetResult()
        {
            Assets = new List<AssetListModel>();
        }
        public List<AssetListModel> Assets { get; set; }
        public int RowsCount { get; set; }
    }
}