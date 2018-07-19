using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using NAVWMSAPI.Models;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using NAVWMSAPI.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace NAVWMSAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NAVWMSController : ApiController
    {
        WMSRepo repo = new WMSRepo();
        // GET: NAVWMS
        [Route("USERLOGIN")]
        [HttpPost]
        public IHttpActionResult USERLOGIN(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string u = "";
                string p = "";
                try
                {
                    // u = ud.Decrypt(ud.userNM);
                    u = ud.userNM;
                }
                catch
                {
                    return Content(HttpStatusCode.InternalServerError, new { message = "Invalid user encription code", result = "", status = false });
                }
                try
                {
                    //p = ud.Decrypt(ud.userPWD);
                    p = ud.userPWD;
                }
                catch
                {
                    return Content(HttpStatusCode.InternalServerError, new { message = "Invalid password encription code", result = "", status = false });
                }
                
                DataSet ds = repo.GetLogin(3, u, p, Convert.ToInt32("0"));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid user or password", result = "", status = false });
                }
                else
                {
                    return Ok(new { message = "Success", result = ds, status = true });
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, new { message = "Invalid user or password", result = "", status = false });
            }
        }

        [Route("PDOCLIST")]
        [HttpPost]
        public IHttpActionResult PDOCLIST(  BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                


                DataSet ds =repo.GetDoclist(Convert.ToInt32(ud.userID));

                if (ds.Tables[0].Rows.Count == 0)
                {
                    
                    return Content(HttpStatusCode.NotFound, new { message = "no data", result = "", status = false });
                }
                else
                {
                    
                    return Ok(new { message = "Success", result = ds, status = true });
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, new { message = "no data", result = "", status = false });
            }
        }


        [Route("PTADOCLIST")]
        [HttpPost]
        public IHttpActionResult PTADOCLIST(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DataSet ds = repo.GETPTADOCLIST(Convert.ToInt32(ud.userID), ud.TokenID);
                if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "1")
                {
                    return Ok(new { message = "Success", result = ds.Tables[0], status = true });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "-7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not found", result = "", status = false });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid Token", result = "", status = false });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { message = "No Data found", result = "", status = false });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("PTA01")]
        [HttpPost]
        public IHttpActionResult PTA01(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DataSet ds = repo.GETPTALISTCONFIRM(Convert.ToInt32(ud.userID), ud.PutNo, ud.BinNo, ud.Barcode, ud.ItemNo, ud.TokenID, Convert.ToInt32(ud.Status) );
                if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "1")
                {
                    return Ok(new { message = "Success", result = ds.Tables[0], status = true });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "-1")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid put data found", result = ds, status = false });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid Token", result = "", status = false });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Incorrect Data : ItemNo or Barcode", result = "", status = false });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("PIK01")]
        [HttpPost]
        public IHttpActionResult PIK01(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DataSet ds = repo.PIKLISTCONFIRM(Convert.ToInt32(ud.userID), ud.PickNo, ud.BinNo, ud.Tag, ud.SBcode, ud.Barcode, ud.ItemNo, ud.TokenID, Convert.ToInt32(ud.Status));
                if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "1")
                {
                    return Ok(new { message = "Success", result = ds.Tables[0], status = true });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "-1")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid pick data", result = "", status = false });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid Token", result = "", status = false });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not valid check:PickNo/ItemNo/Barcode", result = "", status = false });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [Route("CC02")]
        [HttpPost]
        public IHttpActionResult CC02(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DataSet ds = repo.CCTEST(Convert.ToInt32(ud.userID), ud.CCNO , ud.BinNo, ud.ZoneID, ud.Barcode, ud.ItemNo, ud.TokenID, Convert.ToInt32(ud.Status));
                if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "1")
                {
                    return Ok(new { message = "Success", result = ds.Tables[0], status = true });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "-1")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not Valid Please Check:- ItemNo/Barcode/CCNO/BIN", result = "", status = false });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid Token", result = "", status = false });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not Valid Please Check:- ItemNo/Barcode/CCNO/BIN", result = "", status = false });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("CC01")]
        [HttpPost]
        public IHttpActionResult CC01(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DataSet ds = repo.CCLISTCONFIRM(Convert.ToInt32(ud.userID), ud.CCNO, ud.BinNo, ud.ZoneID, ud.Barcode, ud.ItemNo, ud.TokenID, Convert.ToInt32(ud.Status));
                if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "1")
                {
                    return Ok(new { message = "Success", result = ds.Tables[0], status = true });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "-1")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not Valid Please Check:- ItemNo/Barcode/CCNO/BIN", result = "", status = false });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid Token", result = "", status = false });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not Valid Please Check:- ItemNo/Barcode/CCNO/BIN", result = "", status = false });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("SIGNOUT")]
        [HttpPost]
        [AcceptVerbs("SIGNOUT")]
        public IHttpActionResult USERLOGOUT(BAL ud)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                string u = "";
                string p = "";

                DataSet ds = repo.GetLogin(4, u, p, Convert.ToInt32(ud.userID));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid User ID ", result = "", status = false });
                 
                }
                else
                {
                 return Ok(new { message = "Success", result = ds, status = true });
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, new { message = "Invalid user or password", result = "", status = false });
            }
        }

        [Route("MOV01")]
        [HttpPost]
        public IHttpActionResult MOV01(BAL ud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DataSet ds = repo.MOVLISTCONFIRM(Convert.ToInt32(ud.userID), ud.DocNo, ud.BinNo, ud.Barcode, ud.SRNO, ud.ItemNo, ud.TokenID, ud.WHNO, Convert.ToInt32(ud.Status));
                if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "1")
                {
                    return Ok(new { message = "Success", result = ds.Tables[0], status = true });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "-1")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid data", result = "", status = false });
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0][0]) == "7")
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Invalid Token", result = "", status = false });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { message = "Not valid :- ItemNo/ Barcode or already scaned", result = "", status = false });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}