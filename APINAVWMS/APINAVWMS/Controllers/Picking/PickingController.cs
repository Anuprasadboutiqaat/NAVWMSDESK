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

namespace APINAVWMS.Controllers.Picking
{
    public class PickingController : ApiController
    {
        DBTEST dbt = new DBTEST();
        SqlCommand cmd = new SqlCommand();


        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostWMS_PICKING(List<BAL_PUSHDATA.PICKING_CLS> wMS_PICKING)
        {
            try
            {
                IEnumerable<string> headerValues;
                var TokenValue = string.Empty;

                if (Request.Headers.TryGetValues("Token", out headerValues))
                {
                    TokenValue = headerValues.FirstOrDefault();
                }
                if (TokenValue == "BNW@Pick!")
                {
                    List<BAL_PUSHDATA.PICKING_CLS> NAVDATA = new List<BAL_PUSHDATA.PICKING_CLS>();
                    int count = wMS_PICKING.Count;
                    for (int i = 0; i < count; i++)
                    {
                        BAL_PUSHDATA.PICKING_CLS P = new BAL_PUSHDATA.PICKING_CLS();
                        P.H_DOCNO = wMS_PICKING[i].H_DOCNO;
                        P.H_PICKNO = wMS_PICKING[i].H_PICKNO;
                        P.H_WHNO = wMS_PICKING[i].H_WHNO;
                        List<BAL_PUSHDATA.RESPONSE_DETAIL> PTDTL = new List<BAL_PUSHDATA.RESPONSE_DETAIL>();
                        int detailcount = wMS_PICKING[i].PICKINGDETAIL_LIST.Count;
                        for (int j = 0; j < detailcount; j++)
                        {
                            List<BAL_PUSHDATA.RESPONSE_DETAIL> PTDTL1 = new List<BAL_PUSHDATA.RESPONSE_DETAIL>();
                            BAL_PUSHDATA.PICKING_CLS P1 = new BAL_PUSHDATA.PICKING_CLS();
                            string DOCNO = wMS_PICKING[i].H_DOCNO;
                            string PICKNO = wMS_PICKING[i].H_PICKNO;
                            string WHNO = wMS_PICKING[i].H_WHNO;
                            string ZONEID = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_ZONEID;
                            string EANCODE = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_EANCODE;
                            string BINID = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_BINID;
                            string BARCODE = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_BARCODE;
                            string ITEMNO = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_ITEMNO;
                            string SERIALNO = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_SERIALNO;
                            string QTY = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_QTY;
                            string DESCRIPTION = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_DESCRIPTION;
                            string UOM = wMS_PICKING[i].PICKINGDETAIL_LIST[j].D_UOM;
                            try
                            {
                                cmd.CommandText = "WMS_NAVDATA_PULL";
                                cmd.Parameters.AddWithValue("@STATUS", 2);
                                cmd.Parameters.AddWithValue("@WHNO", WHNO);
                                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                                cmd.Parameters.AddWithValue("@SUBDOCNO", PICKNO);
                                cmd.Parameters.AddWithValue("@BARCODE", BARCODE);
                                cmd.Parameters.AddWithValue("@ITEMNO", ITEMNO);
                                cmd.Parameters.AddWithValue("@SERAILNO", SERIALNO);
                                cmd.Parameters.AddWithValue("@ITEMDESC", DESCRIPTION);
                                cmd.Parameters.AddWithValue("@UOM", UOM);
                                cmd.Parameters.AddWithValue("@QTY", QTY);
                                cmd.Parameters.AddWithValue("@BINNO", BINID);
                                cmd.Parameters.AddWithValue("@ZONEID", ZONEID);
                                cmd.Parameters.AddWithValue("@EANCODE", EANCODE);
                                int k = dbt.EXECUTENONQUERY_PROCE_FUNCTION(cmd);
                                cmd.Parameters.Clear();
                                //if (k > 0)
                                {
                                    P.H_DOCNO = wMS_PICKING[i].H_DOCNO;
                                    P.H_PICKNO = wMS_PICKING[i].H_PICKNO;
                                    P.H_WHNO = wMS_PICKING[i].H_WHNO;
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
                                P.H_DOCNO = wMS_PICKING[i].H_DOCNO;
                                P.H_PICKNO = wMS_PICKING[i].H_PICKNO;
                                P.H_WHNO = wMS_PICKING[i].H_WHNO;
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
                    //Exception ex = null;
                    var message = string.Format("Invalid Token");
                    //LogError(ex, message);
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
            string doc = "Docno:" + docno;
            try
            {
                dbt.EXECUTENONQUERY_FUNCT("INSERT INTO ERRORMGMT_API (APPLICATION,API,APITYPE,CONTROLLER,ERROR_TYPE,ERROR_MESSAGE,ERROR_DETAILS,ERROR_LINE,ERROR_TIME,REMARKS) VALUES ('NAVWMS','APINAVWMS','Consume','Picking','" + ex.Source.ToString().Trim() + "','" + exmsg + "','" + exdetails + "','" + line + "',GETDATE(),'" + doc + "')");
            }
            catch { }
        }
    }
}
