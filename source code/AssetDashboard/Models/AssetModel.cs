using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarTrack.Dashboard.Models
{
    public class AssetModel
    {
        public int Id { get; set; }
        public bool SubAsset { get; set; }
        public bool MultibleSubSelected { get; set; }
        public string Name { get; set; }
        public string TagId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseDateString
        {
            get
            {
                if (PurchaseDate != null)
                    return PurchaseDate.ToString("yyyy-MM-dd");
                return string.Empty;
            }
        }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public string SerialNumber { get; set; }
       
       
        public DateTime ExpireDate { get; set; }
        public string ExpireDateString 
        { 
            get 
            {
                if (ExpireDate != null)
                    return ExpireDate.ToString("yyyy-MM-dd");
                return string.Empty;
            } 
        }

        public string SubAssetListTagId0 { get; set; }
        public string SubAssetListTagId1 { get; set; }
        public string SubAssetListTagId2 { get; set; }
        public string SubAssetListTagId3 { get; set; }
        public string SubAssetListTagId4 { get; set; }
        public string SubAssetListTagId5 { get; set; }
        public string SubAssetListTagId6 { get; set; }
        public string SubAssetListTagId7 { get; set; }
        public string SubAssetListTagId8 { get; set; }
        public string SubAssetListTagId9 { get; set; }

        public string SubAssetListName0 { get; set; }
        public string SubAssetListName1 { get; set; }
        public string SubAssetListName2 { get; set; }
        public string SubAssetListName3 { get; set; }
        public string SubAssetListName4 { get; set; }
        public string SubAssetListName5 { get; set; }
        public string SubAssetListName6 { get; set; }
        public string SubAssetListName7 { get; set; }
        public string SubAssetListName8 { get; set; }
        public string SubAssetListName9 { get; set; }



        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteTagId { get; set; }
        public string SiteDetail { get; set; }
        public string SiteStreet { get; set; }
        public string SiteCity { get; set; }
        public string SiteState { get; set; }
        public string SitePostalCode { get; set; }
        public string SiteCountry { get; set; }
        public SiteStatus SiteStatus { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int DepartmentId { get; set; }
        
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationDetail { get; set; }

        public string TypeName { get; set; }
        public string TypeDetail { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDetail { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDetail { get; set; } 

        public AssetStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public AssetImage AssetImage { get; set; }
        public List<SubAsetModel> SubAssets { get; set; }
        public string AssetId { get;  set; }
    }

    
    
    public class AssetFullModel
    {
        public IEnumerable<Asset> Assets { get; set; }
        public SelectList SiteList { get; set; }
        public SelectList CategoryList { get; set; }
        public SelectList LocationList { get; set; }
        public SelectList TypeList { get; set; }
        public SelectList DepartmentList { get; set; }
        public AssetStatus Status { get; set; }
    }
    public class FullList
    {
        public SelectList Location { get; set; }
        public SelectList Site { get; set; }
        public SelectList Reader { get; set; } 
    }

    public enum AssetStatus
    {
        Normal= 1,
        Expired=3
    }
}