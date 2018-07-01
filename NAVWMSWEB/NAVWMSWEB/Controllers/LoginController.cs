using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NAVWMSWEB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public int SetSessionValues(string userid, string username, string rollid, string tokenid, string stid)
        {
            Session["USERID"] = userid;
            Session["USERNAME"] = username;
            Session["ROLLID"] = rollid;
            Session["TOKENID"] = tokenid;
            Session["STOREID"] = stid;
            return 1;
           
        }
    }
}