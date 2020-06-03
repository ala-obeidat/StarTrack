using StarTrack.Dashboard.Models;
using StarTrack.Dashboard.Models.Search;
using StarTrack.Dashboard.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StarTrack.Dashboard.Controllers
{
    public class HomeController : BaseController
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            var locations = db.Locations.ToList();
            return View(locations);
        }

        [HttpGet]
        public ActionResult List(GridModel model)
        {

            var search = FixData._rentoSerializer.Deserialize<TransactionSearch>(model.Settings);

            var response = ListMovements(model, search);
            IEnumerable<string[]> ResultListData = response.AssetTransactions.Select(o => new string[]
                    {
                        o.Direction=="IN "?"<i title='IN' class='icons in fas fa-download'></i>":"<i class='icons out fas fa-upload' title='OUT'></i>",
                        o.Location,
                            o.AssetId.ToString(),
                            "false",
                            o.AssetName,
                            ((AssetStatus)o.AssetStatus).ToString(),

                            o.Date_and_Time.ToString("dd/MM/yyyy hh:mm tt"),

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
    }
}