using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NAVWMSDESK.Models;
using NAVWMSDESK.ViewModel.Login;
using System.DirectoryServices;
namespace NAVWMSDESK.Controllers
{
   
    public class LoginController : Controller
    {
        NAVWMSEntities db = new NAVWMSEntities();
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(LoginViewModel Users)
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
        [HttpPost]
        public int  GetUserCredentials(string UserName,string Password)
        {
            var RESULT = (from a in db.WMS_USER_EDITOR
                          join b in db.WMS_ROLL_EDITOR
                          on a.ROLLID equals b.ROLLID
                          where a.USERNAME == UserName && a.USERPASSWORD == Password
                          select new
                          {
                              a.USERNAME,a.STOREID,a.USERID,
                              b.ROLLDESC
                          }).SingleOrDefault();
            if (RESULT != null)
            {
                Session["USERID"] = RESULT.USERID.ToString();
                Session["USERNAME"] = RESULT.USERNAME.ToString();
                Session["STOREID"] = RESULT.STOREID.ToString();
                Session["ROLL"] = RESULT.ROLLDESC.ToString();
                return 1;
            }
            else
            {
                return 0;
            }

        }

    }
}