using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StarTrack.Dashboard.Models;
using System.IO;
using System.Data.Entity.Validation;
using System.Collections.ObjectModel;
using StarTrack.Dashboard.Shared;
using StarTrack.Dashboard.Models.Search;

namespace StarTrack.Dashboard.Controllers
{

    public class AssetsController : BaseController
    {


        // GET: Assets
        [HttpGet]
        public ActionResult Index()
        {
            var model = new AssetFullModel();
            model.Assets = null;
            model.CategoryList = new SelectList(db.Categories, "Id", "Name");
            model.LocationList = new SelectList(db.Locations, "Id", "Name");
            model.DepartmentList = new SelectList(db.Departments, "Id", "Name");
            model.SiteList = new SelectList(db.Sites, "Id", "Name");
            model.TypeList = new SelectList(db.Types, "Id", "Name");
            return View(model);
        }
        private string GetImagePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "<img src='images/asset.png' width ='50' />";
            return "<img src='Content/" + path.Replace("\\", "/") + "' width= '50' />";
        }
        [HttpGet]
        public ActionResult List(GridModel model)
        {

            var search = FixData._rentoSerializer.Deserialize<AssetSearch>(model.Settings);

            var response = ListAssets(model, search);
            IEnumerable<string[]> ResultListData = response.Assets.Select(o => new string[]
                    {
                            $"<a class='btn btn-info' href='javaScript:viewAsset({o.Id})' title='View Or Edit'>{o.AID}</a>",
                            "false",
                              GetImagePath(o.ImagePath),
                            o.Name,
                            o.Location,
                            o.TagId,
                            o.Category,
                            o.Department,
                            o.Type,
                            o.Cost.ToString(),
                            o.PurchaseDate.ToString("dd/MM/yyyy"),
                            ((AssetStatus)o.Status).ToString(),

                            o.CreateDate.ToString("dd/MM/yyyy HH:mm"),

                });
            return Json(new GridReponseSuccess()
            {
                sEcho = model.sEcho,
                iTotalRecords = response.RowsCount,
                iTotalDisplayRecords = response.RowsCount,
                aaData = ResultListData,
                ErrorCode = ErrorCode.Success
            }, JsonRequestBehavior.AllowGet);
        }
        // GET: Assets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = await db.Assets.FindAsync(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            var model = new AssetModel();
            model.AssetImage = new AssetImage();
            if (asset.AssetImage != null)
            {
                model.AssetImage.Path = Url.Content("~/Content/" + asset.AssetImage.Path.Replace("\\", "/"));
            }
            else
            {
                model.AssetImage.Path = Url.Content("~/images/asset.png");
            }
            model.Id = asset.Id;
            model.Name = asset.Name;
            model.CategoryId = asset.CategoryId;
            model.Details = asset.Details;
            model.TypeId = asset.TypeId;
            model.SiteId = asset.SiteId;
            model.AssetId = asset.AssetId;
            model.LocationId = asset.LocationId;
            model.DepartmentId = asset.DepartmentId.HasValue ? asset.DepartmentId.Value : 0;
            if (asset.ExpireDate.HasValue)
                model.ExpireDate = asset.ExpireDate.Value;
            if (asset.PurchaseDate.HasValue)
                model.PurchaseDate = asset.PurchaseDate.Value;
            if (asset.SubAsset)
            {
                model.SubAsset = true;
                if (asset.MultibleSubSelected.HasValue)
                    model.MultibleSubSelected = asset.MultibleSubSelected.Value;
                if (asset.SubAssets != null && asset.SubAssets.Any())
                {
                    model.SubAssets = new List<SubAsetModel>();
                    foreach (var item in asset.SubAssets)
                    {
                        model.SubAssets.Add(new SubAsetModel()
                        {
                            Name = item.Name,
                            TagId = item.TagId
                        });
                    }
                }
            }

            if (asset.Cost.HasValue)
                model.Cost = asset.Cost.Value;
            model.SerialNumber = asset.SerialNumber;
            model.TagId = asset.TagId;
            model.Status = (AssetStatus)asset.Status;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

         

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(AssetModel asset)
        {
            try
            {
                bool edit = false;
                if (asset == null)
                {
                    return HttpNotFound();
                }
                Asset dbAsset = null;
                if (asset.Id == 0)
                {
                    dbAsset = new Asset();
                }
                else
                {
                    edit = true;
                    dbAsset = await db.Assets.FindAsync(asset.Id).ConfigureAwait(true);
                }
                dbAsset.Name = asset.Name;
                dbAsset.AssetId = asset.AssetId;
                dbAsset.Status = (int)asset.Status;
                dbAsset.TagId = asset.TagId;
                dbAsset.Details = asset.Details;
                if (asset.ExpireDate != DateTime.MinValue)
                    dbAsset.ExpireDate = asset.ExpireDate;
                dbAsset.SerialNumber = asset.SerialNumber;
                if (asset.PurchaseDate != DateTime.MinValue)
                    dbAsset.PurchaseDate = asset.PurchaseDate;
                dbAsset.Cost = asset.Cost;
                if (asset.SubAsset)
                {
                    dbAsset.MultibleSubSelected = asset.MultibleSubSelected;
                    dbAsset.SubAsset = true;
                    if (dbAsset.SubAssets != null && dbAsset.SubAssets.Any())
                    {
                        var subList = new List<SubAsset>();
                        foreach (var sub in dbAsset.SubAssets)
                        {
                            subList.Add(sub);
                        }
                        foreach (var item in subList)
                        {
                            db.SubAssets.Remove(item);
                        }
                        subList.Clear();
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName0))
                    {
                        dbAsset.SubAssets = new Collection<SubAsset>();
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName0,
                            TagId = asset.SubAssetListTagId0,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName1))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName1,
                            TagId = asset.SubAssetListTagId1,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName2))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName2,
                            TagId = asset.SubAssetListTagId2,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName3))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName3,
                            TagId = asset.SubAssetListTagId3,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName4))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName4,
                            TagId = asset.SubAssetListTagId4,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName5))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName5,
                            TagId = asset.SubAssetListTagId5,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName6))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName6,
                            TagId = asset.SubAssetListTagId6,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName7))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName7,
                            TagId = asset.SubAssetListTagId7,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName8))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName8,
                            TagId = asset.SubAssetListTagId8,
                        });
                    }
                    if (!string.IsNullOrEmpty(asset.SubAssetListName9))
                    {
                        dbAsset.SubAssets.Add(new SubAsset()
                        {
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            Name = asset.SubAssetListName9,
                            TagId = asset.SubAssetListTagId9,
                        });
                    }
                }
                else
                {
                    if (dbAsset.SubAssets != null && dbAsset.SubAssets.Any())
                    {
                        var subList = new List<SubAsset>();
                        foreach (var sub in dbAsset.SubAssets)
                        {
                            subList.Add(sub);
                        }
                        foreach (var item in subList)
                        {
                            db.SubAssets.Remove(item);
                        }
                        subList.Clear();
                    }
                    dbAsset.SubAsset = false;
                    dbAsset.MultibleSubSelected = false;
                }
                if (!string.IsNullOrEmpty(asset.SiteName))
                {
                    dbAsset.SiteId = 0;
                    dbAsset.Site = new Site();
                    dbAsset.Site.City = asset.SiteCity;
                    dbAsset.Site.Country = asset.SiteCountry;
                    dbAsset.Site.PostalCode = asset.SitePostalCode;
                    dbAsset.Site.Name = asset.SiteName;
                    dbAsset.Site.State = asset.SiteState;
                    dbAsset.Site.Status = (int)asset.SiteStatus;
                    dbAsset.Site.Street = asset.SiteStreet;
                    dbAsset.Site.TagId = asset.SiteTagId;
                    dbAsset.Site.Detail = asset.SiteDetail;
                    dbAsset.Site.CreatedDate = DateTime.Now;
                }
                else
                {
                    dbAsset.SiteId = asset.SiteId;
                }
                if (!string.IsNullOrEmpty(asset.TypeName))
                {
                    dbAsset.TypeId = 0;
                    dbAsset.Type = new Models.Type();
                    dbAsset.Type.Name = asset.TypeName;
                    dbAsset.Type.Detail = asset.TypeDetail;
                    dbAsset.Type.CreateDate = DateTime.Now;
                }
                else
                {
                    dbAsset.TypeId = asset.TypeId;
                }
                if (!string.IsNullOrEmpty(asset.CategoryName))
                {
                    dbAsset.CategoryId = 0;
                    dbAsset.Category = new Models.Category();
                    dbAsset.Category.Name = asset.CategoryName;
                    dbAsset.Category.Detail = asset.CategoryDetail;
                    dbAsset.Category.CreatedDate = DateTime.Now;
                }
                else
                {
                    
                        dbAsset.CategoryId = asset.CategoryId;
                }
                if (!string.IsNullOrEmpty(asset.DepartmentName))
                {
                    dbAsset.DepartmentId = 0;
                    dbAsset.Department = new Models.Department();
                    dbAsset.Department.Name = asset.DepartmentName;
                    dbAsset.Department.Detail = asset.DepartmentDetail;
                    dbAsset.Department.CreateDate = DateTime.Now;
                }
                else
                {
                    if (asset.DepartmentId != 0)
                        dbAsset.DepartmentId = asset.DepartmentId;
                }


                if (!string.IsNullOrEmpty(asset.LocationName))
                {
                    dbAsset.LocationId = 0;
                    dbAsset.Location = new Models.Location();
                    dbAsset.Location.Name = asset.LocationName;
                    dbAsset.Location.Detail = asset.LocationName;
                    dbAsset.Location.CreatedDate = DateTime.Now;
                    if (string.IsNullOrEmpty(asset.SiteName))
                        dbAsset.Location.SiteId = asset.SiteId;
                    else
                        dbAsset.Location.Site = dbAsset.Site;
                    if (!edit)
                    {
                        dbAsset.Location1 = dbAsset.Location;
                    }
                }
                else
                {
                    dbAsset.LocationId = asset.LocationId;
                    if (!edit)
                    {
                        dbAsset.CurrentLocation = asset.LocationId;
                    }
                }

                if (asset.ImageFile != null)
                {
                    var imagePath = Path.Combine(Server.MapPath("~/Content"), Session["USERNAME"].ToString());
                    var imageName = $"{Guid.NewGuid()}_{asset.ImageFile.FileName}";
                    var dbImagePath = Path.Combine(Session["USERNAME"].ToString(), imageName);
                    var imageFullPath = Path.Combine(imagePath, imageName);
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
                    if (System.IO.File.Exists(imageFullPath))
                    {
                        System.IO.File.Delete(imageFullPath);
                    }
                    asset.ImageFile.SaveAs(imageFullPath);
                    if (edit)
                    {
                        dbAsset.AssetImage = await db.AssetImages.FindAsync(asset.Id).ConfigureAwait(true);
                        if (dbAsset.AssetImage == null)
                        {
                            dbAsset.AssetImage = new AssetImage();
                            dbAsset.AssetImage.CreatedDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        dbAsset.AssetImage = new AssetImage();
                        dbAsset.AssetImage.CreatedDate = DateTime.Now;
                    }
                    dbAsset.AssetImage.Name = asset.ImageFile.FileName;
                    dbAsset.AssetImage.Size = (int)asset.ImageFile.InputStream.Length;
                    dbAsset.AssetImage.Path = dbImagePath;
                    dbAsset.AssetImage.Status = 1;
                }
                if (edit)
                {
                    db.Entry(dbAsset).State = EntityState.Modified;
                }
                else
                {

                    dbAsset.CreatedDate = DateTime.Now;
                    db.Assets.Add(dbAsset);
                }
                await db.SaveChangesAsync();
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

         
        
    }
}
