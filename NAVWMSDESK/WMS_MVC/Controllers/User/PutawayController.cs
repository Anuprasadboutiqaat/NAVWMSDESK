using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

using WMS_MVC.Models;
using WMS_MVC.ViewModel.Putaway;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
namespace WMS_MVC.Controllers.User
{
    public class PutawayController : Controller
    {
        NAVWMSEntities db = new NAVWMSEntities();
        public ActionResult Putaway()
        {
            BindViewData();
            return View();
        }
        public void BindViewData()
        {
            List<SelectListItem> STATUS = new List<SelectListItem>();
            STATUS.Add(new SelectListItem { Text = "OPEN", Value = "0" });
            STATUS.Add(new SelectListItem { Text = "PARTIAL", Value = "1" });
            STATUS.Add(new SelectListItem { Text = "COMPLETE", Value = "2" });
            ViewData["STATUS"] = STATUS;

            List<SelectListItem> STATUS1 = new List<SelectListItem>();
            STATUS1.Add(new SelectListItem { Text = "ASSIGN", Value = "0" });
            STATUS1.Add(new SelectListItem { Text = "UNASSIGN", Value = "1" });
            STATUS1.Add(new SelectListItem { Text = "BOTH", Value = "2" });
            ViewData["STATUS1"] = STATUS1;

            var USER = new SelectList(db.WMS_USER_EDITOR.ToList(), "USERID", "USERNAME");
            ViewData["USER"] = USER.ToList();

            var ZONE = new SelectList(db.WMS_ZONE_EDITOR.ToList(), "ZONEID", "ZONEDESC");
            ViewData["ZONE"] = ZONE.ToList();

        }
        public ActionResult GetPutaway([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from a in db.WMS_PUTAWAY
                          select new PutawayViewModel
                          {
                              DOCNO = a.DOCNO,
                              USERASSIGNDATE = a.USERASSIGNDATE,
                              RECINSERTED=a.RECINSERTED,
                              STATUS = a.PUTSTATUS,
                              PUTNO = a.PUTNO
                          }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Putaway_Assignment()
        {
           
            string doc = Request.QueryString["d"].ToString();
            BindViewData();
            // return View("~/Views/Putaway/Putaway_Assignment.cshtml"); 
            return View("~/Views/Putaway/Putaway_Assignment.cshtml");
        }
        public ActionResult GetPutawayDetails([DataSourceRequest]DataSourceRequest request, string id)
        {
            if (id != "")
            {                               
                var result = (from a in db.WMS_PUTAWAY                            
                              where a.DOCNO == id
                              select new PutawayViewModel
                              {                                 
                                  PUTNO = a.PUTNO,
                                  ASSIGNSTATUS = a.ASSIGNSTATUS,
                                  USERID = a.USERID
                               }).ToList();
                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("test", JsonRequestBehavior.AllowGet);
            }

        }
        //public ActionResult BulkUpdate(string doc, List<PutawayViewModel> details)
        //{
        //    string msg = null;
        //    foreach (PutawayViewModel det in details)
        //    {
        //        WMS_PUTAWAY ab = new Models.WMS_PUTAWAY();
        //        ab.ASSIGNSTATUS = det.ASSIGNSTATUS;
        //    }
        //    return Json(msg);
        //}
        public ActionResult BulkUpdate(string [] ids, PutawayViewModel header)
        {
            string msg = null;

            foreach (string s in ids)
            {
                string abc = s.ToString();
            }
            //foreach (PutawayViewModel det in ids)
            //{
            //    string putno = ids.ToString();
            //    //WMS_PUTAWAY ab = new Models.WMS_PUTAWAY();
            //    //ab.ASSIGNSTATUS = det.ASSIGNSTATUS;
            //}
            return Json(msg);
        }
    }
}