using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NAVWMSWEB.Models;
using NAVWMSWEB.Repository ;
using System.Net;
using System.IO;
using System.Text;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data;

namespace NAVWMSWEB.Controllers.Putaway
{
    public class Put01Controller : Controller
    {
        // GET: Put01
        NAVWMSREPO repo = new NAVWMSREPO();
        public ActionResult Put01()
        {
            return View("~/Views/Putaway/Put01.cshtml");
        }
        public ActionResult Put02()
        {
            return View("~/Views/Putaway/Put02.cshtml");
        }
        public ActionResult Put03()
        {
            return View("~/Views/Putaway/Put03.cshtml");
        }
    }
}