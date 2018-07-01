using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NAVWMSDESK.Models;
using NAVWMSDESK.ViewModel.Login;
using System.DirectoryServices;
namespace NAVWMSDESK.Controllers.User
{
    public class Login1Controller : Controller
    {
        // GET: Login1
        public ActionResult Login1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login1(LoginViewModel Users)
        {
            string dominName = string.Empty;
            string adPath = string.Empty;
            string strError = string.Empty;
            if (AuthenticateUser("LDAP://btq.local", Users.UserName, Users.Password) == true)
            {

                ViewData["Msgstatus"] = "Success";
            }
            else
            {
                ViewData["Msgstatus"] = "AD";

            }


            return View();
        }
        public bool AuthenticateUser(string path, string user, string pass)
        {
            DirectoryEntry de = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);
            try
            {
                DirectorySearcher ds = new DirectorySearcher(de);
                ds.FindOne();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}