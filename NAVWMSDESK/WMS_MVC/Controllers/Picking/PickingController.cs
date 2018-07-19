using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.IO;
using System.Text;
using NAVWMSDESK.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;
using NAVWMSDESK.ViewModel.Picking;
using WMS_MVC.Models;

namespace NAVWMSDESK.Controllers.Picking
{
    public class PickingController : Controller
    {
        NAVWMSEntities db = new NAVWMSEntities();
        SqlCommand CMD = new SqlCommand();
        DBTEST dt = new DBTEST();
        public ActionResult Picking()
        {
            BindViewData();
            return View();
        }
        public ActionResult GetPicking([DataSourceRequest]DataSourceRequest request, string DATE1, string DATE2, string ZONEID)
        {

            DateTime date1 = Convert.ToDateTime(DATE1);
            DateTime date2 = Convert.ToDateTime(DATE2);
            string startdate = date1.ToString("yyyy-MMM-dd");
            string enddate = date2.ToString("yyyy-MMM-dd");
            CMD.CommandText = "WMS_DESKTOP";
            CMD.Parameters.AddWithValue("@STATUS", 3);
            CMD.Parameters.AddWithValue("@DATE1", startdate);
            CMD.Parameters.AddWithValue("@DATE2", enddate);
            CMD.Parameters.AddWithValue("@WHNO", ZONEID);
            DataTable DT = dt.EXECUTEDATATABLE_PROCE_FUNCT(CMD);
            CMD.Parameters.Clear();
            var result = DT;
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            
        }
        
        public void BindViewData()
        {
            List<SelectListItem> STATUS = new List<SelectListItem>();
            STATUS.Add(new SelectListItem { Text = "OPEN", Value = "0" });
            STATUS.Add(new SelectListItem { Text = "INPROGRESS", Value = "1" });
            STATUS.Add(new SelectListItem { Text = "COMPLETE", Value = "2" });
            ViewData["STATUS"] = STATUS;

            List<SelectListItem> STATUS1 = new List<SelectListItem>();
            STATUS1.Add(new SelectListItem { Text = "ASSIGN", Value = "0" });
            STATUS1.Add(new SelectListItem { Text = "UNASSIGN", Value = "1" });
            STATUS1.Add(new SelectListItem { Text = "BOTH", Value = "2" });
            ViewData["STATUS1"] = STATUS1;

            var USER = new SelectList(db.WMS_USER_EDITOR.ToList(), "USERID", "USERNAME");
            ViewData["USER"] = USER.ToList();

            //var ZONE = new SelectList(db.WMS_ZONE_EDITOR.ToList(), "ZONEID", "ZONEDESC");
            //ViewData["ZONE"] = ZONE.ToList();

        }
        public ActionResult Picking_Assignment()
        {          
            
            List<SelectListItem> ZONEID = new List<SelectListItem>();
            ZONEID.Add(new SelectListItem { Text = "test", Value = "0" });

            ViewData["ZONEID"] = ZONEID;
            
            BindViewData();           
            return View("~/Views/Picking/Picking_Assignment.cshtml");

        }

        public ActionResult GetPickingDetails([DataSourceRequest]DataSourceRequest request, string id, string WHNO)
        {
            CMD.CommandText = "WMS_DESKTOP";
            CMD.Parameters.AddWithValue("@STATUS", 4);
            if (id != "")
            {
                CMD.Parameters.AddWithValue("@DOCNO", id);
            }
            else
            {
                CMD.Parameters.AddWithValue("@DOCNO", "");
            }
            CMD.Parameters.AddWithValue("@WHNO", WHNO);
            DataTable DT = dt.EXECUTEDATATABLE_PROCE_FUNCT(CMD);
            CMD.Parameters.Clear();
            var result = DT;
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BulkUpdate(string[] ids, PickingViewModel header)
        {
            string msg = null;
            foreach (string s in ids)
            {
                //string PICKNO = s.ToString();
                string[] vars = s.Split(',');
                string PICKNO = vars[0];
                string DOCNO = vars[1];
                CMD.CommandText = "WMS_DESKTOP";
                CMD.Parameters.AddWithValue("@STATUS", 5);
                CMD.Parameters.AddWithValue("@USERID", header.USER);
                CMD.Parameters.AddWithValue("@USERNAME", header.USERNAME);                
                CMD.Parameters.AddWithValue("@DOCNO", DOCNO);
                CMD.Parameters.AddWithValue("@WHNO", header.WHNO);
                CMD.Parameters.AddWithValue("@SUBDOCNO", PICKNO);
                CMD.Parameters.AddWithValue("@DOCTYPE", "PICK");
                dt.EXECUTENONQUERY_PROCE_FUNCT(CMD);
                CMD.Parameters.Clear();

            }
            return Json(msg);
        }
        public ActionResult GetUser()
        {
            return Json ( db.WMS_USER_EDITOR.AsEnumerable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZoneID(string id, string WHNO)
        {
            if (id != "")
            {
                var result = (from a in db.WMS_PICKING
                              where a.DOCNO == id && a.WAREHOUSENO == WHNO
                              select new PickingViewModel
                              {
                                  ZONEID = a.ZONEID

                              }).Distinct().ToList();
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = (from a in db.WMS_PICKING
                              where a.WAREHOUSENO == WHNO
                              select new PickingViewModel
                              {
                                  ZONEID = a.ZONEID

                              }).Distinct().ToList();
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            
            
        }
    }
}