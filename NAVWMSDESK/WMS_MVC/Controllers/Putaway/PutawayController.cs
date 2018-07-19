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
using NAVWMSDESK.ViewModel.Putaway;
using WMS_MVC.Models;

namespace NAVWMSDESK.Controllers.Putaway
{
    public class PutawayController : Controller
    {
        NAVWMSEntities db = new NAVWMSEntities();
        SqlCommand CMD = new SqlCommand();
        DBTEST dt = new DBTEST();
        public ActionResult Putaway()
        {
            BindViewData();
            return View();
        }
        public ActionResult GetPutaway([DataSourceRequest]DataSourceRequest request, string DATE1, string DATE2, string ZONEID)
        {
            //var result = (from a in db.WMS_PUTAWAY
            //              select new PutawayViewModel
            //              {
            //                  DOCNO = a.DOCNO,
            //                  USERASSIGNDATE = a.USERASSIGNDATE,
            //                  RECINSERTED = a.RECINSERTED,
            //                  STATUS = a.PUTSTATUS,
            //                  PUTNO = a.PUTNO
            //              }).ToList();
            DateTime date1 = Convert.ToDateTime(DATE1);
            DateTime date2 = Convert.ToDateTime(DATE2);
            string startdate = date1.ToString("yyyy-MMM-dd");
            string enddate = date2.ToString("yyyy-MMM-dd");
            CMD.CommandText = "WMS_DESKTOP";
            CMD.Parameters.AddWithValue("@STATUS", 0);
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
            ViewData["STATUS1"] = STATUS1;
            
            var USER = new SelectList(db.WMS_USER_EDITOR.ToList(), "USERID", "USERNAME");
            ViewData["USER"] = USER.ToList();

            //var ZONE = new SelectList(db.WMS_ZONE_EDITOR.ToList(), "ZONEID", "ZONEDESC");
            //ViewData["ZONE"] = ZONE.ToList();

        }
        public ActionResult Putaway_Assignment()
        {          
            
            List<SelectListItem> ZONEID = new List<SelectListItem>();
            ZONEID.Add(new SelectListItem { Text = "test", Value = "0" });

            ViewData["ZONEID"] = ZONEID;
            //var result1 = (from a in db.WMS_PUTAWAY                         
            //              join c in db.WMS_USER_EDITOR
            //              on a.USERID equals c.USERID
            //              where a.DOCNO == id
            //              select new PutawayViewModel
            //              {

            //                  USERID = a.USERID,
            //                  USERNAME = c.USERNAME

            //              }).Distinct().ToList();

            //var USER = new SelectList(result1, "USERID", "USERNAME");
            //ViewData["USER"] = USER.ToList();
            BindViewData();           
            return View("~/Views/Putaway/Putaway_Assignment.cshtml");
        }
        public ActionResult GetPutawayDetails([DataSourceRequest]DataSourceRequest request, string id, string WHNO)
        {
            CMD.CommandText = "WMS_DESKTOP";
            CMD.Parameters.AddWithValue("@STATUS", 1);
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

            //var result = (from a in db.WMS_PUTAWAY
            //              join c in db.WMS_USER_EDITOR on a.USERID equals c.USERID into ps
            //              from c in ps.DefaultIfEmpty()
            //              where a.DOCNO == id 
            //                select new PutawayViewModel
            //                {
            //                    PUTNO = a.PUTNO,
            //                    ASSIGNSTATUS = a.ASSIGNSTATUS,
            //                    USERID = a.USERID,
            //                    USERNAME = c == null ? "None" : c.USERNAME
            //                }).ToList();
            //return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPutawayDetails1([DataSourceRequest]DataSourceRequest request, string id, string type, string value)
        {
            if (id != "")
            {
                if (type== "user") {

                   
                    if (value == "")
                    {
                        var result = (from a in db.WMS_PUTAWAY
                                      join c in db.WMS_USER_EDITOR on a.USERID equals c.USERID into ps
                                      from c in ps.DefaultIfEmpty()
                                      where a.DOCNO == id 
                                      select new PutawayViewModel
                                      {
                                          PUTNO = a.PUTNO,
                                          ASSIGNSTATUS = a.ASSIGNSTATUS,
                                          USERID = a.USERID,
                                          USERNAME = c == null ? "None" : c.USERNAME
                                      }).ToList();
                        return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var user = Convert.ToInt32(value);
                        var result = (from a in db.WMS_PUTAWAY
                                      join c in db.WMS_USER_EDITOR on a.USERID equals c.USERID into ps
                                      from c in ps.DefaultIfEmpty()
                                      where a.DOCNO == id && a.USERID == user
                                      select new PutawayViewModel
                                      {
                                          PUTNO = a.PUTNO,
                                          ASSIGNSTATUS = a.ASSIGNSTATUS,
                                          USERID = a.USERID,
                                          USERNAME = c == null ? "None" : c.USERNAME
                                      }).ToList();
                        return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }
                }
                else if (type == "status")
                {
                    if (value == "2" || value == "")
                    {
                        
                        var result = (from a in db.WMS_PUTAWAY
                                      join c in db.WMS_USER_EDITOR on a.USERID equals c.USERID into ps
                                      from c in ps.DefaultIfEmpty()
                                      where a.DOCNO == id
                                      select new PutawayViewModel
                                      {
                                          PUTNO = a.PUTNO,
                                          ASSIGNSTATUS = a.ASSIGNSTATUS,
                                          USERID = a.USERID,
                                          USERNAME = c == null ? "None" : c.USERNAME
                                      }).ToList();
                        return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }
                    else if (value == "1")
                    {

                        var result = (from a in db.WMS_PUTAWAY
                                      join c in db.WMS_USER_EDITOR on a.USERID equals c.USERID into ps
                                      from c in ps.DefaultIfEmpty()
                                      where a.DOCNO == id && c.USERNAME == null
                                      select new PutawayViewModel
                                      {
                                          PUTNO = a.PUTNO,
                                          ASSIGNSTATUS = a.ASSIGNSTATUS,
                                          USERID = a.USERID,
                                          USERNAME = c == null ? "None" : c.USERNAME
                                      }).ToList();
                        return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }
                    else if(value == "0" )
                    {
                        var result = (from a in db.WMS_PUTAWAY
                                      join c in db.WMS_USER_EDITOR on a.USERID equals c.USERID 
                                      where a.DOCNO == id
                                      select new PutawayViewModel
                                      {
                                          PUTNO = a.PUTNO,
                                          ASSIGNSTATUS = a.ASSIGNSTATUS,
                                          USERID = a.USERID,
                                          USERNAME = c.USERNAME
                                      }).ToList();
                        return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("test", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("test", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("test", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult BulkUpdate(string[] ids, PutawayViewModel header)
        {
            string msg = null;
             
            foreach (string s in ids)
            {
                //string PUTNO = s.ToString();
                string[] vars = s.Split(',');
                string PUTNO = vars[0];
                string DOCNO = vars[1];
                //if (DUMUSER == "null")
                //{
                //    DUMUSER = "0";
                //}
                CMD.CommandText = "WMS_DESKTOP";
                CMD.Parameters.AddWithValue("@STATUS", 2);
                CMD.Parameters.AddWithValue("@USERID", header.USER);
                CMD.Parameters.AddWithValue("@USERNAME", header.USERNAME);                
                CMD.Parameters.AddWithValue("@DOCNO", DOCNO);
                CMD.Parameters.AddWithValue("@WHNO", header.WHNO);
                CMD.Parameters.AddWithValue("@SUBDOCNO", PUTNO);
                CMD.Parameters.AddWithValue("@DOCTYPE", "PUT");
                //CMD.Parameters.AddWithValue("@DUMUSERID", DUMUSER);
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
                var result = (from a in db.WMS_PUTAWAY
                              where a.DOCNO == id && a.WAREHOUSENO == WHNO
                              select new PutawayViewModel
                              {
                                  ZONEID = a.ZONEID

                              }).Distinct().ToList();
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = (from a in db.WMS_PUTAWAY
                              where a.WAREHOUSENO == WHNO
                              select new PutawayViewModel
                              {
                                  ZONEID = a.ZONEID

                              }).Distinct().ToList();
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            


            //var ZONEID = new SelectList(db.WMS_ZONE_EDITOR.ToList(), "ZONEID", "ZONEDESC");
            ////ViewData["ZONEID"] = ZONEID.ToList();

            //return Json(ZONEID, JsonRequestBehavior.AllowGet);
        }
    }
}