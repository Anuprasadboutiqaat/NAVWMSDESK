﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using WMS_MVC.Models;

namespace WMS_MVC.Views.TestService
{
    public class tryyController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }

    public class WMS_CLASS_EDITORController : ApiController
    {
        private NAVWMSEntities db = new NAVWMSEntities();

        public DataSourceResult GetWMS_CLASS_EDITOR([System.Web.Http.ModelBinding.ModelBinder(typeof(WebApiDataSourceRequestModelBinder))]DataSourceRequest request)
        {
            return db.WMS_CLASS_EDITOR.ToDataSourceResult(request);
        }

        public WMS_CLASS_EDITOR GetWMS_CLASS_EDITOR(int id)
        {
            WMS_CLASS_EDITOR wMS_CLASS_EDITOR = db.WMS_CLASS_EDITOR.Find(id);
            if(wMS_CLASS_EDITOR == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return wMS_CLASS_EDITOR;
        }

        public HttpResponseMessage PutWMS_CLASS_EDITOR(int id, WMS_CLASS_EDITOR wMS_CLASS_EDITOR)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != wMS_CLASS_EDITOR.CLASSID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry(wMS_CLASS_EDITOR).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostWMS_CLASS_EDITOR(WMS_CLASS_EDITOR wMS_CLASS_EDITOR)
        {
            if (ModelState.IsValid)
            {
                db.WMS_CLASS_EDITOR.Add(wMS_CLASS_EDITOR);
                db.SaveChanges();
                
                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { wMS_CLASS_EDITOR },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = wMS_CLASS_EDITOR.CLASSID }));

                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public HttpResponseMessage DeleteWMS_CLASS_EDITOR(int id)
        {
            WMS_CLASS_EDITOR wMS_CLASS_EDITOR = db.WMS_CLASS_EDITOR.Find(id);
            
            if (wMS_CLASS_EDITOR == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                db.WMS_CLASS_EDITOR.Remove(wMS_CLASS_EDITOR);

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, wMS_CLASS_EDITOR);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}