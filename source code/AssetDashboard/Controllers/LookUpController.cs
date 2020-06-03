
using DevExpress.Data.Mask;
using DevExpress.DirectX.Common.Direct2D;
using Newtonsoft.Json;
using StarTrack.Dashboard.Models;
using StarTrack.Dashboard.Models.Search;
using StarTrack.Dashboard.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StarTrack.Dashboard.Controllers
{
    public class LookUpController : BaseController
    {
        #region Index

        // GET: Views
        [HttpGet]
        public ActionResult Index(string id="")
        {
            var model = new FullList();
            model.Location = new SelectList(db.Locations, "Id", "Name");
            model.Reader = new SelectList(db.Readers, "Id", "Name");
            model.Site = new SelectList(db.Sites, "Id", "Name");
            ViewBag.LookUpType = id;
            return View(model);
        }

        #endregion

        #region Save

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(LookUpModel model)
        {

            if (model.Id == 0)
            {
                switch (model.Type)
                {
                    case LookUpType.Category:
                        var category = new Category();
                        category.CreatedDate = DateTime.Now;
                        category.Name = model.Name;
                        category.Status = 1;
                        category.Detail = model.Detail;
                        db.Categories.Add(category);
                        break;
                    case LookUpType.Type:
                        var type = new StarTrack.Dashboard.Models.Type();
                        type.CreateDate = DateTime.Now;
                        type.Name = model.Name;
                        type.Status = 1;
                        type.Detail = model.Detail;
                        db.Types.Add(type);
                        break;
                    case LookUpType.Department:
                        var department = new Department();
                        department.CreateDate = DateTime.Now;
                        department.Name = model.Name;
                        department.Status = 1;
                        department.Detail = model.Detail;
                        db.Departments.Add(department);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (model.Type)
                {
                    case LookUpType.Category:
                        var category = db.Categories.Find(model.Id);
                        db.Entry(category).State = EntityState.Modified;
                        category.Name = model.Name;
                        category.Detail = model.Detail;
                        break;
                    case LookUpType.Type:
                        var type = db.Types.Find(model.Id);
                        type.Name = model.Name;
                        type.Detail = model.Detail;
                        db.Entry(type).State = EntityState.Modified;
                        break;
                    case LookUpType.Department:
                        var department = db.Departments.Find(model.Id);
                        department.Name = model.Name;
                        department.Detail = model.Detail;
                        db.Entry(department).State = EntityState.Modified;
                        break;
                    default:
                        break;
                }
            }
            db.SaveChanges();

            return RedirectToAction("Index", new { id = model.Type.ToString() });
             
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveLocation(Location model)
        {

            if (model.Id == 0)
            {
                var location = new Location();
                location.CreatedDate = DateTime.Now;
                location.Name = model.Name;
                location.Status = 1;
                location.InAsset = model.InAsset;
                location.OutAsset = model.OutAsset;
                location.MissingAsset = model.MissingAsset;
                location.TotalAsset = model.TotalAsset;
                location.Detail = model.Detail;
                location.SiteId = model.SiteId;
                db.Locations.Add(location);
            }
            else
            {
                var location = db.Locations.Find(model.Id);
                location.Name = model.Name;
                location.InAsset = model.InAsset;
                location.OutAsset = model.OutAsset;
                location.MissingAsset = model.MissingAsset;
                location.TotalAsset = model.TotalAsset;
                location.Detail = model.Detail;
                location.SiteId = model.SiteId;
                db.Entry(location).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { id = "Location" }); 
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveSite(Site model)
        {

            if (model.Id == 0)
            {
                var site = new Site();
                site.CreatedDate = DateTime.Now;
                site.Name = model.Name;
                site.Status = 1;
                site.PostalCode = model.PostalCode;
                site.State = model.State;
                site.Street = model.Street;
                site.TagId = model.TagId;
                site.Detail = model.Detail;
                site.City = model.City;
                site.Country = model.Country;
                db.Sites.Add(site);
            }
            else
            {
                var site = db.Sites.Find(model.Id);
                site.Name = model.Name;
                site.PostalCode = model.PostalCode;
                site.State = model.State;
                site.Street = model.Street;
                site.TagId = model.TagId;
                site.Detail = model.Detail;
                site.City = model.City;
                site.Country = model.Country;
                db.Entry(site).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { id = "Site" }); 
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveReader(Reader model)
        {

            if (model.Id == 0)
            {
                var item = new Reader();
                item.CreatedDate = DateTime.Now;
                item.Status = 1;
                item.Name = model.Name;
                item.AntinnaCount = model.AntinnaCount;
                item.Type = model.Type;
                item.Detail = model.Detail;
                item.SiteId = model.SiteId;
                db.Readers.Add(item);
            }
            else
            {
                var item = db.Readers.Find(model.Id);
                item.Name = model.Name;
                item.AntinnaCount = model.AntinnaCount;
                item.Type = model.Type;
                item.Detail = model.Detail;
                item.SiteId = model.SiteId;
                db.Entry(item).State = EntityState.Modified;
            }
            db.SaveChanges();  
            return RedirectToAction("Index", new { id = "Reader" });

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveAntenna(Antinna model)
        {

            if (model.Id == 0)
            {
                var item = new Antinna();
                item.CreatedDate = DateTime.Now;
                item.Name = model.Name;
                item.Status = 1;
                item.LocationId = model.LocationId;
                item.Type = model.Type;
                item.Detail = model.Detail;
                item.ReaderId = model.ReaderId;
                db.Antinnas.Add(item);
            }
            else
            {
                var item = db.Antinnas.Find(model.Id);
                item.LocationId = model.LocationId;
                item.Type = model.Type;
                item.Detail = model.Detail;
                item.ReaderId = model.ReaderId;
                db.Entry(item).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { id = "Antenna" });  
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveUser(User model)
        {

            if (model.Id == 0)
            {
                var item = new User();
                item.CreateDate = DateTime.Now;
                item.Username = model.Username;
                item.Flag = model.Flag;
                item.Password = model.Password;
                item.MobileNumber = model.MobileNumber;
                item.FullName = model.FullName;
                db.Users.Add(item);
            }
            else
            {
                var item = db.Users.Find(model.Id);
                item.CreateDate = DateTime.Now;
                item.Username = model.Username;
                item.Flag = model.Flag;
                if (!string.IsNullOrEmpty(model.Password))
                    item.Password = model.Password;
                item.MobileNumber = model.MobileNumber;
                item.FullName = model.FullName;
                db.Entry(item).State = EntityState.Modified;
            }
            db.SaveChanges(); 
            return RedirectToAction("Index",new {id="User" });
        }

        #endregion

        #region List

        [HttpGet]
        public ActionResult List(GridModel model)
        {

            var search = FixData._rentoSerializer.Deserialize<LookUpSearch>(model.Settings);
            var pageSize = model == null ? 10 : ((model.iDisplayLength == 0) ? 10 : model.iDisplayLength);
            var PageNumber = model == null ? 1 : ((model.iDisplayStart == 0) ? 1 : model.iDisplayStart) / pageSize + 1;

            List<LookUpBase> result = null;
            int rowCount = 0;
            switch (search.Type)
            {
                case LookUpType.Category:
                    rowCount = db.Categories.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name));
                    result = db.Categories.Select(c => new LookUpBase() { CreatedDate = c.CreatedDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.Type:
                    rowCount = db.Types.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()));
                    result = db.Types.Select(c => new LookUpBase() { CreatedDate = c.CreateDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.Department:
                    rowCount = db.Departments.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()));
                    result = db.Departments.Select(c => new LookUpBase() { CreatedDate = c.CreateDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.Location:
                    rowCount = db.Locations.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()));
                    result = db.Locations.Select(c => new LookUpBase() { CreatedDate = c.CreatedDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.Site:
                    rowCount = db.Sites.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()));
                    result = db.Sites.Select(c => new LookUpBase() { CreatedDate = c.CreatedDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.Antenna:
                    rowCount = db.Antinnas.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()));
                    result = db.Antinnas.Select(c => new LookUpBase() { CreatedDate = c.CreatedDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.Reader:
                    rowCount = db.Readers.Count(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()));
                    result = db.Readers.Select(c => new LookUpBase() { CreatedDate = c.CreatedDate, Detail = c.Detail, Id = c.Id, Name = c.Name })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LookUpType.User:
                    rowCount = db.Users.Count(w => string.IsNullOrEmpty(search.Name) || w.Username.ToLower().Contains(search.Name.ToLower()));
                    result = db.Users.Select(c => new LookUpBase() { CreatedDate = c.CreateDate, Detail = c.FullName, Id = c.Id, Name = c.Username })
                        .Where(w => string.IsNullOrEmpty(search.Name) || w.Name.ToLower().Contains(search.Name.ToLower()))
                        .OrderBy(s => s.Id).Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    break;
            }
            IEnumerable<string[]> ResultListData = result.Select(o => new string[]
                    {
                         $"<a class='btn btn-info' href='javaScript:viewLookUp({o.Id})' title='View Or Edit'>{o.Id}</a>",
                          "false",
                            o.Name,
                            o.Detail.Length > 50 ? o.Detail.Substring(0,50)+"...": o.Detail,
                            o.CreatedDate.ToString("dd/MM/yyyy hh:mm tt"),

                });
            return Json(new GridReponseSuccess()
            {
                sEcho = model.sEcho,
                iTotalRecords = rowCount,
                iTotalDisplayRecords = rowCount,
                aaData = ResultListData,
                ErrorCode = ErrorCode.Success
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Detail
        public ActionResult Details(int id, int type)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            object data = null;
            LookUpType lookUpType = (LookUpType)type;
            switch (lookUpType)
            {
                case LookUpType.Category:
                    data = db.Categories.Where(c => c.Id == id).Select(c => new {Id=c.Id,Name=c.Name,Detail=c.Detail });
                    break;
                case LookUpType.Type:
                    data =  db.Types.Where(c => c.Id == id).Select(c => new { Id = c.Id, Name = c.Name, Detail = c.Detail });
                    break;
                case LookUpType.Department:
                    data = db.Departments.Where(c => c.Id == id).Select(c => new { Id = c.Id, Name = c.Name, Detail = c.Detail });
                    break;
                case LookUpType.Location:
                    data = db.Locations.Where(c => c.Id == id)
                        .Select(c => new { 
                            Id = c.Id, 
                            Name = c.Name, 
                            Detail = c.Detail,
                            InAsset=c.InAsset,
                            OutAsset=c.OutAsset,
                            MissingAsset=c.MissingAsset,
                            TotalAsset=c.TotalAsset,
                            SiteId=c.SiteId
                        });
                    break;
                case LookUpType.Site:
                    data =  db.Sites.Where(c => c.Id == id)
                        .Select(c => new {
                            Id = c.Id,
                            Name = c.Name,
                            Detail = c.Detail,
                            City = c.City,
                            Country = c.Country,
                            PostalCode = c.PostalCode,
                            Street = c.Street,
                            State = c.State,
                            TagId = c.TagId,
                        });
                    break;
                case LookUpType.Antenna:
                    data =  db.Antinnas.Where(c => c.Id == id)
                        .Select(c => new {
                            Id = c.Id,
                            Name = c.Name,
                            Detail = c.Detail, 
                            ReaderId = c.ReaderId,
                            LocationId = c.LocationId, 
                        });
                    break;
                case LookUpType.Reader:
                    data =  db.Readers.Where(c => c.Id == id)
                        .Select(c => new {
                            Id = c.Id,
                            Name = c.Name,
                            Detail = c.Detail,
                            SiteId = c.SiteId,
                            AntinnaCount = c.AntinnaCount,
                        });
                    break;
                case LookUpType.User:
                    data =  db.Users.Where(c => c.Id == id)
                        .Select(c => new {
                            Id = c.Id,
                            Username = c.Username,
                            FullName = c.FullName,
                            MobileNumber = c.MobileNumber,
                            Password = c.Password,
                            Flag = c.Flag,

                        });
                    break;
                default:
                    break;
            }

            //if (data == null)
            //{
            //    return HttpNotFound();
            //}
            //var list = JsonConvert.SerializeObject(data,Formatting.None, new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //});

            //return Content(list, "application/json", Encoding.UTF8);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}