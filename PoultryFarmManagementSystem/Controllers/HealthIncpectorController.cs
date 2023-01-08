using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoultryFarmManagementSystem.Models;

namespace PoultryFarmManagementSystem.Controllers
{
    public class HealthIncpectorController : ApiController
    {
        PoultryFarmManagementSystemEntities10 db = new PoultryFarmManagementSystemEntities10();
        [HttpGet]
        public HttpResponseMessage showPoultries()
        {
            try
            {
                var pltries = db.Poultries.Select(x => new {
                    x.pltry_id,
                    x.pltry_name
                });
                
                return Request.CreateResponse(HttpStatusCode.OK, pltries);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage vaccacc(int pltryid)
        {
            try
            {
                var batch = db.LayerBatches.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.lb_id,
                    m.lb_totalcost,
                    m.lb_totalqty,
                    m.pltry_id,
                    m.lb_name
                }).ToList();
                var batchd = db.LayerBatchDatas.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.lb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality,
                    m.sale_status,
                    m.vaccine_name,
                    m.eggs_produced
                }).ToList();
                var j2 = batch.Join(batchd,
                    a => a.lb_id,
                    b => b.lb_id,
                    (a, b) => new
                    {
                        a.lb_id,
                        a.lb_name,
                        b.vaccine_name,
                        b.pltry_id
                    });



                //var join = from f in j2
                //           join s in j3 on f.pltry_id equals s.pltry_id
                //           select new { f,
                //               Name = f.lb_name,
                //               Vaccine = f.vaccine_name,
                //               s };

                return Request.CreateResponse(HttpStatusCode.OK, j2);

            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage bbVacc(int pltryid)
        {
            try
            {
                //-----bb
                var bbatch = db.BroilerBatches.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.bb_id,
                    m.bb_totalcost,
                    m.bb_totalqty,
                    m.pltry_id,
                    m.bb_name
                }).ToList();
                var bbatchd = db.BroilerBatchDatas.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.bb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality,
                    m.sale_status,
                    m.vaccine_name,
                    m.avgbodyweightingrams
                }).ToList();
                var j3 = bbatch.Join(bbatchd,
                    a => a.bb_id,
                    b => b.bb_id,
                    (a, b) => new
                    {
                        a.bb_id,
                        a.bb_name,
                        b.vaccine_name,
                        b.pltry_id
                    }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, j3);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage notVaccinated()
        {
            try
            {
                //-----bb
                var bbatch = db.BroilerBatches.Select(m => new {
                    m.bb_id,
                    m.bb_totalcost,
                    m.bb_totalqty,
                    m.pltry_id,
                    m.bb_name
                }).ToList();
                var bbatchd = db.BroilerBatchDatas.Where(x => x.vaccine_name == "").Select(m => new {
                    m.bb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality,
                    m.sale_status,
                    m.vaccine_name,
                    m.avgbodyweightingrams
                }).ToList();
                var j2 = bbatch.Join(bbatchd,
                    a => a.bb_id,
                    b => b.bb_id,
                    (a, b) => new
                    {
                        a.bb_id,
                        a.bb_name,
                        b.vaccine_name,
                        b.ageindays,
                        b.pltry_id
                    }).ToList();

                //-----lb
                var lbatch = db.LayerBatches.Select(m => new {
                    m.lb_id,
                    m.lb_totalcost,
                    m.lb_totalqty,
                    m.pltry_id,
                    m.lb_name
                }).ToList();
                var lbatchd = db.LayerBatchDatas.Where(x => x.vaccine_name == "").Select(m => new {
                    m.lb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality,
                    m.sale_status,
                    m.vaccine_name,
                    m.eggs_produced
                }).ToList();
                var j3 = bbatch.Join(bbatchd,
                    a => a.bb_id,
                    b => b.bb_id,
                    (a, b) => new
                    {
                        a.bb_id,
                        a.bb_name,
                        b.vaccine_name,
                        b.ageindays,
                        b.pltry_id
                    }).ToList();

                var join = from f in j2
                           join s in j3 on f.pltry_id equals s.pltry_id
                           select new
                           {
                               f,s
                           };
                return Request.CreateResponse(HttpStatusCode.OK, join);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage Notvacc()
        {
            try
            {
                var lbatchd = db.LayerBatchDatas.Where(z=>z.vaccine_name=="").Select(m => new {
                    m.pltry_id,
                    m.ageindays,
                    m.vaccine_name
                }).ToList();
                var allplt = db.Poultries.Select(x=>x);
                var j_lb = from t1 in lbatchd
                        join t2 in allplt on t1.pltry_id equals t2.pltry_id
                        select new{
                            t1.pltry_id,
                            t1.ageindays,
                            t1.vaccine_name,
                            t2.pltry_name
                        };


                //--BB
                var bbatchd = db.BroilerBatchDatas.Where(z => z.vaccine_name == "").Select(m => new {
                    m.pltry_id,
                    m.ageindays,
                    m.vaccine_name
                }).ToList();
                var j_bb = from t1 in bbatchd
                           join t2 in allplt on t1.pltry_id equals t2.pltry_id
                           select new
                           {
                               t1.pltry_id,
                               t1.ageindays,
                               t1.vaccine_name,
                               t2.pltry_name
                           };
                var t_j = j_lb.Concat(j_bb);


                return Request.CreateResponse(HttpStatusCode.OK, t_j);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public HttpResponseMessage vacc()
        {
            try
            {
                var lbatchd = db.LayerBatchDatas.Where(m=>m.vaccine_name!="").Select(m => new {
                    m.pltry_id,
                    m.ageindays,
                    m.vaccine_name
                }).ToList();
                var allplt = db.Poultries.Select(x => x);
                var j_lb = from t1 in lbatchd
                           join t2 in allplt on t1.pltry_id equals t2.pltry_id
                           select new
                           {
                               t1.pltry_id,
                               t1.ageindays,
                               t1.vaccine_name,
                               t2.pltry_name
                           };


                //--BB
                var bbatchd = db.BroilerBatchDatas.Where(m => m.vaccine_name != "").Select(m => new {
                    m.pltry_id,
                    m.ageindays,
                    m.vaccine_name
                }).ToList();
                var j_bb = from t1 in bbatchd
                           join t2 in allplt on t1.pltry_id equals t2.pltry_id
                           select new
                           {
                               t1.pltry_id,
                               t1.ageindays,
                               t1.vaccine_name,
                               t2.pltry_name
                           };
                var t_j = j_lb.Concat(j_bb);


                return Request.CreateResponse(HttpStatusCode.OK, t_j);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
