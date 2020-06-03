
using StarTrack.Dashboard.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Linq;
using System.Web.Security;
using System.Web;

namespace StarTrack.Dashboard.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private RFIDEntities _context = new RFIDEntities();
        // GET: Account
        
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpGet] 
        public ActionResult SignOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        [HttpPost] 
        public ActionResult LoginPost(LoginModel model)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.Username.Equals(model.Username,StringComparison.OrdinalIgnoreCase) && u.Password.Equals(model.Password));
                if (user == null)
                {
                    ViewBag.Error = "Invalid Username Or Password"; 
                }
                else if ((user.Flag & 1) == 0)
                {
                    ViewBag.Error = "User not Active";
                }
                else
                {
                    Session["USERNAME"] = user.Username;
                    Session["FULLNAME"] = user.FullName;
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = "General Error";
            }
            return View("Login");
        }
    }
}