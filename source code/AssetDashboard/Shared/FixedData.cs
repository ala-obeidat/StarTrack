 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace StarTrack.Dashboard.Shared
{
    public class FixData
    {
        public static readonly JavaScriptSerializer _rentoSerializer = new JavaScriptSerializer();
        public static bool IsRTL
        {
            get
            {
                return HttpContext.Current.Session["M_LANGUAGE"].Equals("ar-SA");
            }
        }
        public static bool IsLogin
        {
            get
            {
                return HttpContext.Current.Session["USERNAME"] != null;
            }

        }
        
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["USERNAME"] == null)
                    return string.Empty;
                return (string)HttpContext.Current.Session["USERNAME"];
            }
        }
        
    }
}