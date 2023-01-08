using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoultryFarmManagementSystem.Models;
namespace PoultryFarmManagementSystem.Controllers
{
    public class LoginSignupController : ApiController
    {
        PoultryFarmManagementSystemEntities10 db = new PoultryFarmManagementSystemEntities10();
        //----------Login 
        [HttpGet]
        public HttpResponseMessage userlogin(String email,String password)
        {
            try
            {
                var login = db.Users.FirstOrDefault(x => x.user_email == email && x.user_password == password);
                var worker = db.Workers.FirstOrDefault(y => y.worker_email == email && y.worker_password == password);
                if (login!=null)
                {
                    if (login.user_type==0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Customer");
                    }
                    else if (login.user_type==1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Owner");
                    }
                    else if (login.user_type == 2)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Admin");
                    }
                    else if (login.user_type == 3)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "HealthIncpector");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid");
                    }
                }
                else if (worker!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Worker");
                }else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Not Found");
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-------------Signup 
        [HttpPost]
        public HttpResponseMessage Signup(User u)
        {
            try
            {
                db.Users.Add(u);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, u);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------User Info
        [HttpGet]
        public HttpResponseMessage userInfo(String email)
        {
            try
            {
                var ui = db.Users.Where(x => x.user_email == email).Select(s=> new {
                    s.user_id,
                    s.user_name,
                    s.user_email,
                    s.user_address,
                    s.user_phoneno,
                    s.user_type
                });
                return Request.CreateResponse(HttpStatusCode.OK, ui);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------Worker Info
        [HttpGet]
        public HttpResponseMessage workerInfo(String email)
        {
            try
            {
                var wi = db.Workers.Where(x => x.worker_email == email).Select(s => new {
                    s.worker_id,
                    s.worker_email,
                    s.worker_name,
                    s.worker_address,
                    s.worker_phoneno,
                    s.user_id,
                    s.pltry_id
                });
                return Request.CreateResponse(HttpStatusCode.OK, wi);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------For Testing
        [HttpGet]
        public HttpResponseMessage Test()
        {
            try
            {
                var login = db.Users.Select(x=>x).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, login);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}
