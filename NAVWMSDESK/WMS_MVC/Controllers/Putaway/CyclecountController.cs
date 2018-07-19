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
using NAVWMSDESK.ViewModel;
using NAVWMSDESK.ViewModel.Cyclecount;
using System.Net;
using Newtonsoft.Json;
using WMS_MVC.Models;

namespace NAVWMSDESK.Controllers.Cyclecount
{
    public class CyclecountController : Controller
    {
        NAVWMSEntities db = new NAVWMSEntities();
        SqlCommand CMD = new SqlCommand();
        DBTEST dt = new DBTEST();
        int flag = 0;
        public ActionResult Cyclecounts()
        {
            BindViewData();
            return View();
        }
        public ActionResult CyclecountList()
        {
            BindViewData();
            return View();
        }
        public ActionResult CyclecountStatus()
        {
            BindViewData();
            
            return View("~/Views/Cyclecount/CyclecountStatus.cshtml");

        }
        public void BindViewData()
        {
            List<SelectListItem> ZONEID = new List<SelectListItem>();
            ZONEID.Add(new SelectListItem { Text = "test", Value = "0" });

            ViewData["ZONEID"] = ZONEID;

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
        }

        public ActionResult GetCyclecount([DataSourceRequest]DataSourceRequest request, string DATE1, string DATE2, string ZONEID)
        {
            
            DateTime date1 = Convert.ToDateTime(DATE1);
            DateTime date2 = Convert.ToDateTime(DATE2);
            string startdate = date1.ToString("yyyy-MMM-dd");
            string enddate = date2.ToString("yyyy-MMM-dd");
            CMD.CommandText = "WMS_DESKTOP";
            CMD.Parameters.AddWithValue("@STATUS", 8);
            CMD.Parameters.AddWithValue("@DATE1", startdate);
            CMD.Parameters.AddWithValue("@DATE2", enddate);
            CMD.Parameters.AddWithValue("@WHNO", ZONEID);
            DataTable DT = dt.EXECUTEDATATABLE_PROCE_FUNCT(CMD);
            CMD.Parameters.Clear();
            var result = DT;
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        public ActionResult CycleCount_Assignment()
        {
            
            List<SelectListItem> ZONEID = new List<SelectListItem>();
            ZONEID.Add(new SelectListItem { Text = "test", Value = "0" });

            ViewData["ZONEID"] = ZONEID;
            
            BindViewData();
            return View("~/Views/Cyclecount/CyclecountStatus.cshtml");
        }

        [HttpPost]
        public ActionResult Cyclecounts(CyclecountViewModel header)
        {
            string binno = header.BINNO;
            if (binno != null)
            {
                try
                {
                    string[] strArr = binno.Split(',');
                    for (int i = 0; i <= strArr.Length - 1; i++)
                    {
                        string SQL = "INSERT INTO WMS_CC_REQUEST (DOCDATE,ZONEID,BINNO,USERID,ACHIVE) VALUES('" + header.DATE1 + "','" + header.ZONEID + "','" + strArr[i] + "','" + header.USER + "','0')";
                        dt.EXECUTENONQUERY_FUNCT(SQL);
                    }
                    flag = 1;
                }
                catch
                {
                    flag = 0;
                }
            }
            else
            {
                try
                {
                    string zoneid = header.ZONEID;
                    string[] strArr = zoneid.Split(',');
                    for (int i = 0; i <= strArr.Length - 1; i++)
                    {
                        string SQL = "INSERT INTO WMS_CC_REQUEST (DOCDATE,ZONEID,BINNO,USERID,ACHIVE) VALUES('" + header.DATE1 + "','" + strArr[i] + "',null,'" + header.USER + "','0')";
                        dt.EXECUTENONQUERY_FUNCT(SQL);
                    }
                    flag = 1;
                }
                catch
                {
                    flag = 0;
                }

            }
            // this.AddToastMessage("Success", "Done !!", ToastType.Success);           
            return Json(flag);
        }
    
        public ActionResult GetCyclecountDetails([DataSourceRequest]DataSourceRequest request, string id, string WHNO)
        {

                CMD.CommandText = "WMS_DESKTOP";
                CMD.Parameters.AddWithValue("@STATUS", 6);
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
        public ActionResult BulkUpdate(string[] ids, CyclecountViewModel header)
        {
            string msg = null;
            foreach (string s in ids)
            {
                //string CCNO = s.ToString();
                string[] vars = s.Split(',');
                string CCNO = vars[0];
                string DOCNO = vars[1];
                
                CMD.CommandText = "WMS_DESKTOP";
                CMD.Parameters.AddWithValue("@STATUS", 7);
                CMD.Parameters.AddWithValue("@USERID", header.USER);
                CMD.Parameters.AddWithValue("@USERNAME", header.USERNAME);
                CMD.Parameters.AddWithValue("@DOCNO", DOCNO);
                CMD.Parameters.AddWithValue("@WHNO", header.WHNO);
                CMD.Parameters.AddWithValue("@SUBDOCNO", CCNO);
                CMD.Parameters.AddWithValue("@DOCTYPE", "CycCount");
                //CMD.Parameters.AddWithValue("@DUMUSERID", DUMUSER);
                dt.EXECUTENONQUERY_PROCE_FUNCT(CMD);
                CMD.Parameters.Clear();

            }
            return Json(msg);
        }
        public ActionResult GetUser()
        {
            return Json(db.WMS_USER_EDITOR.AsEnumerable(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetZoneID(string id, string WHNO)
        {
            if(id != "")
                {
                    var result = (from a in db.WMS_CYCLECOUNT
                                  where a.DOCNO == id && a.WAREHOUSENO == WHNO
                                  select new CyclecountViewModel
                                  {
                                      ZONEID = a.ZONEID

                                  }).Distinct().ToList();
                    return Json(result.ToList(), JsonRequestBehavior.AllowGet);
                }
            else
                {
                    var result = (from a in db.WMS_CYCLECOUNT
                                  where  a.WAREHOUSENO == WHNO
                                  select new CyclecountViewModel
                                  {
                                      ZONEID = a.ZONEID

                                  }).Distinct().ToList();
                    return Json(result.ToList(), JsonRequestBehavior.AllowGet);
                }
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file, CyclecountViewModel header)
        {
            //string zone, bin;
            int flag = 0;
            if (file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/assets/docs"), fileName + DateTime.Now.ToString("dd-MMM-yyyy_hh-mm-ss") + ".txt");
                file.SaveAs(path);
                StreamReader SR = new StreamReader(path);

                CMD.CommandText = "WMS_NAVDATA_PULL";
                CMD.Parameters.AddWithValue("@STATUS", 4);

                var DOCNO = dt.EXECUTESCALAR_PROCE_FUNCT(CMD);
                CMD.Parameters.Clear();

                if (header.ZONENBIN == "ZONENBIN")
                {
                    while (SR.Peek() >= 0)
                    {
                        string text;
                        text = SR.ReadLine();
                        //string[] parts = text.Split(new char[] { ',' });
                        //zone = parts[0];
                        //bin = parts[1];
                        if (!string.IsNullOrEmpty(text))
                        {
                            CMD.CommandText = "WMS_NAVDATA_PULL";
                            CMD.Parameters.AddWithValue("@STATUS", 5);
                            CMD.Parameters.AddWithValue("@DOCNO", DOCNO);
                            CMD.Parameters.AddWithValue("@WHNO", header.WHNO);
                            CMD.Parameters.AddWithValue("@ZONEID", header.ZONEID);
                            CMD.Parameters.AddWithValue("@BINNO", text);
                            CMD.Parameters.AddWithValue("@USERID", header.USERHOLD);
                            
                            dt.EXECUTENONQUERY_PROCE_FUNCT(CMD);
                            CMD.Parameters.Clear();
                            string SQL = "INSERT INTO WMS_CC_REQUEST (DOCDATE,ZONEID,BINNO,USERID,ACHIVE) VALUES('" + header.DATE1 + "','" + header.ZONEID + "','" + text + "','" + header.USERHOLD + "','0')";
                            dt.EXECUTENONQUERY_FUNCT(SQL);
                            flag = 1;
                        }
                    }
                }
                else
                {
                    while (SR.Peek() >= 0)
                    {
                        string text;
                        text = SR.ReadLine();
                        
                        if (!string.IsNullOrEmpty(text))
                        {
                            CMD.CommandText = "WMS_NAVDATA_PULL";
                            CMD.Parameters.AddWithValue("@STATUS", 5);
                            CMD.Parameters.AddWithValue("@DOCNO", DOCNO);
                            CMD.Parameters.AddWithValue("@WHNO", header.WHNO);
                            CMD.Parameters.AddWithValue("@ZONEID", text);
                            CMD.Parameters.AddWithValue("@BINNO", "NULL");
                            CMD.Parameters.AddWithValue("@USERID", header.USERHOLD);

                            dt.EXECUTENONQUERY_PROCE_FUNCT(CMD);
                            CMD.Parameters.Clear();
                            string SQL = "INSERT INTO WMS_CC_REQUEST (DOCDATE,ZONEID,BINNO,USERID,ACHIVE) VALUES('" + header.DATE1 + "','" + text + "','NULL','" + header.USERHOLD + "','0')";
                            dt.EXECUTENONQUERY_FUNCT(SQL);
                            flag = 2;
                        }

                    }
                }

                CYCLECOUNT_CLS P = new CYCLECOUNT_CLS();

                if (flag == 2)
                {
                    
                    DataTable RESULT1 = dt.EXECUTEDATATABLE_FUNCT("SELECT DOCNO,WAREHOUSENO	FROM WMS_CC_REQUEST WHERE DOCNO='"+ DOCNO + "' and WAREHOUSENO='" + header.WHNO + "' group by DOCNO,WAREHOUSENO");
                    P.REQUESTID = RESULT1.Rows[0]["DOCNO"].ToString();
                    //P.LOCATION = RESULT1.Rows[0]["WAREHOUSENO"].ToString();
                    P.LOCATION = "KW";
                    
                    DataTable RESULTDET = dt.EXECUTEDATATABLE_FUNCT("SELECT ZONEID	FROM WMS_CC_REQUEST WHERE DOCNO='" + DOCNO + "' and WAREHOUSENO='" + header.WHNO + "'");
                    List<lstZONEID> PTDTL = new List<lstZONEID>();
                    int zonecount = RESULTDET.Rows.Count;
                    for (int j = 0; j < zonecount; j++)
                    {
                        
                        lstZONEID DET = new lstZONEID();
                        
                        DET.ZONEID = RESULTDET.Rows[j]["ZONEID"].ToString();
                        
                        PTDTL.Add(DET);
                        
                        P.lstZONEID = PTDTL;
                    }

                }
                if (flag == 1)
                {

                    DataTable RESULT1 = dt.EXECUTEDATATABLE_FUNCT("SELECT DOCNO,WAREHOUSENO	FROM WMS_CC_REQUEST WHERE DOCNO='" + DOCNO + "' and WAREHOUSENO='" + header.WHNO + "' group by DOCNO,WAREHOUSENO");
                    P.REQUESTID = RESULT1.Rows[0]["DOCNO"].ToString();
                    P.LOCATION = RESULT1.Rows[0]["WAREHOUSENO"].ToString();
                    
                    List<lstZONEID> PTDTL = new List<lstZONEID>();
                    
                    for (int j = 0; j < 1; j++)
                    {
                        
                        lstZONEID DET = new lstZONEID();

                        DET.ZONEID = header.ZONEID;

                        PTDTL.Add(DET);

                        P.lstZONEID = PTDTL;
                    }
                    DataTable RESULTBINDET = dt.EXECUTEDATATABLE_FUNCT("SELECT BINNO	FROM WMS_CC_REQUEST WHERE DOCNO='" + DOCNO + "' and WAREHOUSENO='" + header.WHNO + "'");
                    List<lstBINID> BINDTL = new List<lstBINID>();
                    int bincount = RESULTBINDET.Rows.Count;
                    for (int j = 0; j < bincount; j++)
                    {
                        
                        lstBINID DET = new lstBINID();

                        DET.BINID = RESULTBINDET.Rows[j]["BINNO"].ToString();

                        BINDTL.Add(DET);

                        P.lstBINID = BINDTL;
                    }

                }
                var jsonData1 = JsonConvert.SerializeObject(P);
                string json1 = jsonData1;
                try
                {
                    //int sizeOfnavdata = NAVDATA.Count;
                    //if (sizeOfnavdata > 0)
                    //{
                            
                        string webAddr = "http://52.32.169.118:8085/WMSIntegration/GetPutAwayDataFromWMS";
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);


                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        httpWebRequest.PreAuthenticate = true;
                        httpWebRequest.Headers.Add("TokenValue", "B@WMS@at_ERPPut");
                        httpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            var jsonData = JsonConvert.SerializeObject(P);
                            string json = jsonData;

                            //streamWriter.Write(json);
                            streamWriter.Flush();
                        }

                        var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            var responseText = streamReader.ReadToEnd();
                            //List<PUTAWAY_CLS> PUTHEAD = new List<PUTAWAY_CLS>();
                            //var model = JsonConvert.DeserializeObject<List<PUTAWAY_CLS>>(responseText);
                            //int c = model.Count();
                            //for (int i = 0; i < model.Count; i++)
                            //{
                            //    string docno = model[i].DOCNO.ToString();
                            //    string whno = model[i].WAREHOUSENO.ToString();
                            //    string putno = model[i].PUTNO.ToString();
                            //    int count1 = model[i].lstPutAwayDataResponse.Count();
                            //    for (int j = 0; j < count1; j++)
                            //    {
                            //        string itemno = model[i].lstPutAwayDataResponse[j].ITEMNO.ToString();
                            //        string serialno = model[i].lstPutAwayDataResponse[j].SERIALNO.ToString();
                            //        string success = model[i].lstPutAwayDataResponse[j].SUCCESS.ToString();
                            //        if (success == "1")
                            //        {
                            //            dt.EXECUTENONQUERY_FUNCT("UPDATE WMS_PUTAWAY SET PUTSTATUS='3' WHERE DOCNO='" + docno + "'AND PUTNO='" + putno + "' AND WAREHOUSENO='" + whno + "' AND ITEMNO='" + itemno + "' AND SERIALNO='" + serialno + "'");
                            //        }
                            //    }
                            //}
                        }
                    // }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                SR.Close();
            }

            BindViewData();
            ViewData["insert"] = flag;

            return View("~/Views/Cyclecount/Cyclecounts.cshtml");

        }

        

    }
}