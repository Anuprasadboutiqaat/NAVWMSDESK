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
using NAVWMSDESK.ViewModel.Movement;
using NAVWMSDESK.Repository;
using WMS_MVC.Models;

namespace NAVWMSDESK.Controllers.Movement
{
    public class MovementController : Controller
    {
        MovementRepo repo = new MovementRepo();
        NAVWMSEntities db = new NAVWMSEntities();
        SqlCommand CMD = new SqlCommand();
        DBTEST dt = new DBTEST();
        public ActionResult Movement()
        {
            BindViewData();
            return View();
        }
        public void BindViewData()
        {
            List<SelectListItem> STATUS = new List<SelectListItem>();
            STATUS.Add(new SelectListItem { Text = "PICKED", Value = "PICKED" });
            STATUS.Add(new SelectListItem { Text = "GETSUGBIN", Value = "GETSUGBIN" });
            STATUS.Add(new SelectListItem { Text = "PUTINPRO", Value = "PUTINPRO" });
            ViewData["MOVSTATUS"] = STATUS;
            
            var USER = new SelectList(db.WMS_USER_EDITOR.ToList(), "USERID", "USERNAME");
            ViewData["USER"] = USER.ToList();
            
        }
        public ActionResult GetMovement([DataSourceRequest]DataSourceRequest request, MovementViewModel VM)
        {
            DateTime date1 = Convert.ToDateTime(VM.DATE1);
            DateTime date2 = Convert.ToDateTime(VM.DATE2);
            string startdate = date1.ToString("yyyy-MMM-dd");
            string enddate = date2.ToString("yyyy-MMM-dd");
            DataTable dt = repo.GetMovementList (startdate, enddate, VM.WHNO);            
            var result = dt;
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUser()
        {
            return Json(db.WMS_USER_EDITOR.AsEnumerable(), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateMovement([DataSourceRequest]DataSourceRequest request, MovementViewModel mov)
        {
            var result = db.WMS_MOVE_HEAD.SingleOrDefault(b => b.DOCNO == mov.DOCNO);
            if (result != null)
            {
                result.USERID = mov.USERID;
                db.SaveChanges();
            }
            return Json(new[] { mov }.ToDataSourceResult(request, ModelState));
        }
    }
}