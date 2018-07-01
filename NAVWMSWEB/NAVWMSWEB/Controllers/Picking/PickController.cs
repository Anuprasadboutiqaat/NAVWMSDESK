using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NAVWMSWEB.Models;
using NAVWMSWEB.Repository;
using System.Net;
using System.IO;
using System.Text;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data;


namespace NAVWMSWEB.Controllers.Picking
{
    public class PickController : Controller
    {
         
        NAVWMSREPO repo = new NAVWMSREPO();
        public ActionResult Pick01()
        {
            return View("~/Views/Picking/Pick01.cshtml");
        }
        public ActionResult Pick02()
        {
            return View("~/Views/Picking/Pick02.cshtml");
        }
        public ActionResult Pick03()
        {
            return View("~/Views/Picking/Pick03.cshtml");
        }
    }
}