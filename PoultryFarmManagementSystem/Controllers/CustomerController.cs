using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoultryFarmManagementSystem.Models;
namespace PoultryFarmManagementSystem.Controllers
{
    public class CustomerController : ApiController
    {
        PoultryFarmManagementSystemEntities10 db = new PoultryFarmManagementSystemEntities10();
        [HttpGet]
        public HttpResponseMessage broilerOnsale(int pltryid)
        {
            try
            {
                var os = db.Sales.Where(x=>x.lb_id==null&&x.s_status==0).ToList();
                return Request.CreateResponse(HttpStatusCode.OK,os);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Eggs on sale
        [HttpGet]
        public HttpResponseMessage eggsOnsale(int pltryid)
        {
            try
            {
                var os = db.Sales.Where(x => x.bb_id == null && x.s_status == 0 && x.pltry_id==pltryid).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, os);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---Put customer Details
        [HttpPost]
        public HttpResponseMessage bcustomerOrderUpdate(Sale s)
        {
            try
            {
                var os = db.Sales.FirstOrDefault(x => x.sale_id == s.sale_id);
                os.c_name = s.c_name;
                os.c_address = s.c_address;
                os.c_qty = s.c_qty;
                os.s_status = 1;
                //-----Add new Sale
                Sale u = new Sale();
                u.bb_id = os.bb_id;
                u.b_name = os.b_name;
                u.bb_totalqty = os.bb_totalqty - s.c_qty;
                u.bb_avgbodyweight = os.bb_avgbodyweight;
                u.pltry_id = os.pltry_id;
                u.s_status = 0;
                u.bb_priceperchick = os.bb_priceperchick;
                if (u.bb_totalqty!=0)
                {
                    db.Sales.Add(u);
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, os);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---Put customer Details
        [HttpPost]
        public HttpResponseMessage lcustomerOrderUpdate(Sale s)
        {
            try
            {
                var os = db.Sales.FirstOrDefault(x => x.sale_id == s.sale_id);
                os.c_name = s.c_name;
                os.c_address = s.c_address;
                os.c_qty = s.c_qty;
                os.s_status = 1;
                //-----Add new Sale
                Sale u = new Sale();
                u.lb_id = os.lb_id;
                u.b_name = os.b_name;
                u.lb_totaleggsintrays = os.lb_totaleggsintrays - s.c_qty;
                u.pltry_id = os.pltry_id;
                u.s_status = 0;
                u.lb_pricepertryofeggs = os.lb_pricepertryofeggs;
                if (u.lb_totaleggsintrays != 0)
                {
                    db.Sales.Add(u);
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, os);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Pltries
        [HttpGet]
        public HttpResponseMessage bpoultries()
        {
            try
            {
                var z = db.Sales.GroupBy(row=>row.pltry_id).Select(x=>x.FirstOrDefault());
                var join = (from a in db.Poultries
                            join b in z on a.pltry_id equals b.pltry_id
                            select new
                            {
                                a.pltry_id,
                                a.pltry_name,
                                b.sale_id,
                                b.s_status
                            }).Distinct();
               
                
                

                return Request.CreateResponse(HttpStatusCode.OK, join);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}
