using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using NAVWMSDESK.Models;
using NAVWMSDESK.ViewModel.Putaway;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace NAVWMSDESK.Controllers.User
{
    public class SubrollController : Controller
    {
        // GET: Subroll
        public ActionResult Subroll()
        {
            return View();
        }
    }
}