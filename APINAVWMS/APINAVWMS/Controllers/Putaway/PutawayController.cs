using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using APINAVWMS.Repository;
using System.Diagnostics;

namespace APINAVWMS.Controllers.Putaway
{
    public class PutawayController : ApiController
    {
        SqlCommand cmd = new SqlCommand();
        DBTEST dbt = new DBTEST();
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostWMS_PUTAWAY(List<BAL_PUSHDATA.PUTAWAY_CLS> wMS_PUTAWAY)
        {
          
            try
            {
                IEnumerable<string> headerValues;
                var TokenValue = string.Empty;
                if (Request.Headers.TryGetValues("Token", out headerValues))
                {
                    TokenValue = headerValues.FirstOrDefault();
                }
                if (TokenValue == "BNW@Put!")
                {
                    List<BAL_PUSHDATA.PUTAWAY_CLS> NAVDATA = new List<BAL_PUSHDATA.PUTAWAY_CLS>();
                    int count = wMS_PUTAWAY.Count;
                    for (int i = 0; i < count; i++)
                    {
                        BAL_PUSHDATA.PUTAWAY_CLS P = new BAL_PUSHDATA.PUTAWAY_CLS();
                        P.H_DOCNO = wMS_PUTAWAY[i].H_DOCNO;
                        P.H_PUTNO = wMS_PUTAWAY[i].H_PUTNO;
                        P.H_WHNO = wMS_PUTAWAY[i].H_WHNO;
                        List<BAL_PUSHDATA.RESPONSE_DETAIL> PTDTL = new List<BAL_PUSHDATA.RESPONSE_DETAIL>();
                        int detailcount = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST.Count;
                        for (int j = 0; j < detailcount; j++)
                        {
                            List<BAL_PUSHDATA.RESPONSE_DETAIL> PTDTL1 = new List<BAL_PUSHDATA.RESPONSE_DETAIL>();
                            BAL_PUSHDATA.PUTAWAY_CLS P1 = new BAL_PUSHDATA.PUTAWAY_CLS();
                            string DOCNO = wMS_PUTAWAY[i].H_DOCNO;
                            string PUTNO = wMS_PUTAWAY[i].H_PUTNO;
                            string WHNO = wMS_PUTAWAY[i].H_WHNO;
                            string ZONEID = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_ZONEID;
                            string BINID = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_BINID;
                            string BARCODE = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_BARCODE;
                            string ITEMNO = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_ITEMNO;
                            string SERIALNO = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_SERIALNO;
                            string QTY = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_QTY;
                            string DESCRIPTION = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_DESCRIPTION;
                            string UOM = wMS_PUTAWAY[i].PUTAWAYDETAIL_LIST[j].D_UOM;
                            try
                            {
                                cmd.CommandText = "WMS_NAVDATA_PULL";
                                cmd.Parameters.AddWithValue("@STATUS", 1);
                                cmd.Parameters.AddWithValue("@WHNO", WHNO);
                                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                                cmd.Parameters.AddWithValue("@SUBDOCNO", PUTNO);
                                cmd.Parameters.AddWithValue("@BARCODE", BARCODE);
                                cmd.Parameters.AddWithValue("@ITEMNO", ITEMNO);
                                cmd.Parameters.AddWithValue("@SERAILNO", SERIALNO);
                                cmd.Parameters.AddWithValue("@ITEMDESC", DESCRIPTION);
                                cmd.Parameters.AddWithValue("@UOM", UOM);
                                cmd.Parameters.AddWithValue("@QTY", QTY);
                                cmd.Parameters.AddWithValue("@BINNO", BINID);
                                cmd.Parameters.AddWithValue("@ZONEID", ZONEID);
                                int k = dbt.EXECUTENONQUERY_PROCE_FUNCTION(cmd);
                                cmd.Parameters.Clear();
                                //if (k > 0)
                                {
                                    P.H_DOCNO = wMS_PUTAWAY[i].H_DOCNO;
                                    P.H_PUTNO = wMS_PUTAWAY[i].H_PUTNO;
                                    P.H_WHNO = wMS_PUTAWAY[i].H_WHNO;
                                    BAL_PUSHDATA.RESPONSE_DETAIL OBJ_PDTL = new BAL_PUSHDATA.RESPONSE_DETAIL();
                                    OBJ_PDTL.D_SERIALNO = SERIALNO;
                                    OBJ_PDTL.D_ITEMNO = ITEMNO;
                                    OBJ_PDTL.D_MESSAGE = "SUCCESS";
                                    OBJ_PDTL.D_SUCCESS = 1;
                                    PTDTL1.Add(OBJ_PDTL);
                                }

                            }
                            catch (Exception ex)
                            {
                                cmd.Parameters.Clear();
                                P.H_DOCNO = wMS_PUTAWAY[i].H_DOCNO;
                                P.H_PUTNO = wMS_PUTAWAY[i].H_PUTNO;
                                P.H_WHNO = wMS_PUTAWAY[i].H_WHNO;
                                BAL_PUSHDATA.RESPONSE_DETAIL OBJ_PDTL = new BAL_PUSHDATA.RESPONSE_DETAIL();
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
                                PTDTL1.Add(OBJ_PDTL);
                                LogError(ex, P.H_DOCNO);
                            }

                            PTDTL.AddRange(PTDTL1);
                            P.RESPONSE_LIST = PTDTL;

                        }
                        NAVDATA.Add(P);

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
                    //LogError(Exception ex, message);
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
            string doc = "Docno:" + docno;
            try
            {
                dbt.EXECUTENONQUERY_FUNCT("INSERT INTO ERRORMGMT_API (APPLICATION,API,APITYPE,CONTROLLER,ERROR_TYPE,ERROR_MESSAGE,ERROR_DETAILS,ERROR_LINE,ERROR_TIME,REMARKS) VALUES ('NAVWMS','APINAVWMS','Consume','Putaway','" + ex.Source.ToString().Trim() + "','" + exmsg + "','" + exdetails + "','" + line + "',GETDATE(),'" + doc + "')");
            }
            catch { }
        }

    }
}
