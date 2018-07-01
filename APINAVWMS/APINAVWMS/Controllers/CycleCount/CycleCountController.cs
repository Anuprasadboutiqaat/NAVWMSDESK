using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using APINAVWMS.Repository;
using System.Diagnostics;

namespace APINAVWMS.Controllers.CycleCount
{
    public class CycleCountController : ApiController
    {
        DBTEST dbt = new DBTEST();
        SqlCommand cmd = new SqlCommand();
        // GET: CycleCount
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostWMS_Cyclecount(List<BAL_PUSHDATA.DOC_HEADER_CLS> wMS_CycleCount)
        {
            try
            {
                /* header token */
                IEnumerable<string> headerValues;
                var TokenValue = string.Empty;

                if (Request.Headers.TryGetValues("Token", out headerValues))
                {
                    TokenValue = headerValues.FirstOrDefault();
                }
                /* header token ends */
                if (TokenValue == "BNW@Cc!")
                {
                    List<BAL_PUSHDATA.DOC_HEADER_CLS> NAVDATA = new List<BAL_PUSHDATA.DOC_HEADER_CLS>(); // 1)this will used in end of process, 2) collecting in list object Head/Detail and sending final response to the user

                    int count = wMS_CycleCount.Count;

                    for (int i = 0; i < count; i++)
                    {
                        BAL_PUSHDATA.DOC_HEADER_CLS P_objHeadDetail = new BAL_PUSHDATA.DOC_HEADER_CLS();      //  creating object of the class for outer loop 

                        P_objHeadDetail.H_DOCNO = wMS_CycleCount[i].H_DOCNO;
                        P_objHeadDetail.H_SUBDOCNO = wMS_CycleCount[i].H_SUBDOCNO;
                        P_objHeadDetail.H_WHNO = wMS_CycleCount[i].H_WHNO;

                        int detailcount = wMS_CycleCount[i].DOC_DETAIL_LIST.Count;

                        List<BAL_PUSHDATA.RESPONSE_DETAIL> cls_RESPONSE_OUTER = new List<BAL_PUSHDATA.RESPONSE_DETAIL>();    // creating the response class object for response sending to users

                        for (int j = 0; j < detailcount; j++)
                        {
                            List<BAL_PUSHDATA.RESPONSE_DETAIL> cls_RESPONSE_INNER = new List<BAL_PUSHDATA.RESPONSE_DETAIL>();    // cr

                            string DOCNO = wMS_CycleCount[i].H_DOCNO;
                            string SUBDOCNO = wMS_CycleCount[i].H_SUBDOCNO;
                            string WHNO = wMS_CycleCount[i].H_WHNO;
                            string ZONEID = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_ZONEID;
                            string BINNO = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_BINID;
                            string BARCODE = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_BARCODE;
                            string ITEMNO = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_ITEMNO;
                            string SERIALNO = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_SERIALNO;
                            string QTY = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_QTY;
                            string DESCRIPTION = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_DESCRIPTION;
                            string UOM = wMS_CycleCount[i].DOC_DETAIL_LIST[j].D_UOM;
                            try
                            {
                                cmd.CommandText = "WMS_NAVDATA_PULL";
                                cmd.Parameters.AddWithValue("@STATUS", 3);
                                cmd.Parameters.AddWithValue("@WHNO", WHNO);
                                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                                cmd.Parameters.AddWithValue("@SUBDOCNO", SUBDOCNO);
                                cmd.Parameters.AddWithValue("@BARCODE", BARCODE);
                                cmd.Parameters.AddWithValue("@ITEMNO", ITEMNO);
                                cmd.Parameters.AddWithValue("@SERAILNO", SERIALNO);
                                cmd.Parameters.AddWithValue("@ITEMDESC", DESCRIPTION);
                                cmd.Parameters.AddWithValue("@UOM", UOM);
                                cmd.Parameters.AddWithValue("@QTY", QTY);
                                cmd.Parameters.AddWithValue("@BINNO", BINNO);
                                cmd.Parameters.AddWithValue("@ZONEID", ZONEID);
                                int k = dbt.EXECUTENONQUERY_PROCE_FUNCTION(cmd);
                                cmd.Parameters.Clear();

                                BAL_PUSHDATA.RESPONSE_DETAIL OBJ_PDTL = new BAL_PUSHDATA.RESPONSE_DETAIL(); // SIMPLE CLASS OBJECT TO STORE Indivisual 
                                OBJ_PDTL.D_SERIALNO = SERIALNO;
                                OBJ_PDTL.D_ITEMNO = ITEMNO;
                                OBJ_PDTL.D_MESSAGE = "SUCCESS";
                                OBJ_PDTL.D_SUCCESS = 1;
                                cls_RESPONSE_INNER.Add(OBJ_PDTL); // adding SIMPLE CLASS OBJECT to list

                            }
                            catch (Exception ex)
                            {
                                // OnFailuer: applying header and detail to innser response list object
                                cmd.Parameters.Clear();

                                BAL_PUSHDATA.RESPONSE_DETAIL OBJ_PDTL = new BAL_PUSHDATA.RESPONSE_DETAIL(); // SIMPLE CLASS OBJECT TO STORE Indivisual 
                                OBJ_PDTL.D_SERIALNO = SERIALNO;
                                OBJ_PDTL.D_ITEMNO = ITEMNO;
                                if (ex.Message.Contains("PRIMARY KEY constraint"))
                                {
                                    OBJ_PDTL.D_MESSAGE = "Duplicate Record";
                                }
                                else
                                {
                                    OBJ_PDTL.D_MESSAGE = ex.ToString();
                                }
                                OBJ_PDTL.D_SUCCESS = 0;
                                cls_RESPONSE_INNER.Add(OBJ_PDTL); // adding SIMPLE CLASS OBJECT to list
                                LogError(ex, OBJ_PDTL.D_ITEMNO);
                            }
                            cls_RESPONSE_OUTER.AddRange(cls_RESPONSE_INNER);
                            P_objHeadDetail.RESPONSE_LIST = cls_RESPONSE_OUTER;
                        }
                        NAVDATA.Add(P_objHeadDetail);

                    }
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(JArray.FromObject(NAVDATA).ToString(), Encoding.UTF8, "application/json")
                    };
                }

                else
                {

                    var message = string.Format("Invalid Token");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);

                }
            }
            catch (Exception ex)
            {
                var message = string.Format("Error");
                LogError(ex, message);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }
        private void LogError(Exception ex, string docno)
        {


            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(st.FrameCount - 1);
            var line = "Line No:" + frame.GetFileLineNumber();
            string exmsg = ex.Message.ToString().Replace("\'", "");
            string exdetails = null;
            if (ex.InnerException != null)
            {
                exdetails = ex.InnerException.ToString().Replace("\'", "");
            }
            else
            {
                exdetails = "Nil";
            }
            string doc = "ItemNo:" + docno;
            try
            {
                dbt.EXECUTENONQUERY_FUNCT("INSERT INTO ERRORMGMT_API (APPLICATION,API,APITYPE,CONTROLLER,ERROR_TYPE,ERROR_MESSAGE,ERROR_DETAILS,ERROR_LINE,ERROR_TIME,REMARKS) VALUES ('NAVWMS','APINAVWMS','Consume','CycleCount','" + ex.Source.ToString().Trim() + "','" + exmsg + "','" + exdetails + "','" + line + "',GETDATE(),'" + doc + "')");
            }
            catch { }
        }
    }
}