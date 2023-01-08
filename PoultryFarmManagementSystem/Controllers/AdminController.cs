using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoultryFarmManagementSystem.Models;
namespace PoultryFarmManagementSystem.Controllers
{
    public class AdminController : ApiController
    {
        PoultryFarmManagementSystemEntities10 db = new PoultryFarmManagementSystemEntities10();
        //-----All poultries
        [HttpGet]
        public HttpResponseMessage allPoultries()
        {
            try
            {
                var p = db.Poultries.Select(x=>new {
                    x.pltry_id,
                    x.pltry_name,
                    x.pltry_address,
                    x.user_id
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, p);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----All workers
        [HttpGet]
        public HttpResponseMessage allWorkers()
        {
            try
            {
                var w = db.Workers.Select(x => new{
                    x.user_id,
                    x.worker_id,
                    x.worker_email,
                    x.worker_name,
                    x.worker_phoneno,
                    x.worker_address
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, w);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Owners
        [HttpGet]
        public HttpResponseMessage allOwners()
        {
            try
            {
                var own = db.Users.Where(x => x.user_type == 1).Select(m => new
                {
                    m.user_id,
                    m.user_name,
                    m.user_email,
                    m.user_phoneno,
                    m.user_address
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, own);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Customer
        [HttpGet]
        public HttpResponseMessage allCustomer()
        {
            try
            {
                var cus = db.Users.Where(x => x.user_type == 0).Select(m=>new {
                    m.user_id,
                    m.user_email,
                    m.user_name,
                    m.user_phoneno,
                    m.user_type,
                    m.user_address
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, cus);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------Worker reset pass
        //-----All poultries
        [HttpGet]
        public HttpResponseMessage resetworkerpass(String wemail , String pass)
        {
            try
            {
                var p = db.Workers.FirstOrDefault(m => m.worker_email == wemail);
                p.worker_password = pass;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, p);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---Customer reset pass
        [HttpGet]
        public HttpResponseMessage resetcpass(String wemail, String pass)
        {
            try
            {
                var p = db.Users.FirstOrDefault(m => m.user_email == wemail);
                p.user_password = pass;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, p);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}
