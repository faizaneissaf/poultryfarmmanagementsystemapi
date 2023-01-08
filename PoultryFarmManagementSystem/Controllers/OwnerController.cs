using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoultryFarmManagementSystem.Models;

namespace PoultryFarmManagementSystem.Controllers
{
    public class OwnerController : ApiController
    {
        PoultryFarmManagementSystemEntities10 db = new PoultryFarmManagementSystemEntities10();
        [HttpGet]
        public HttpResponseMessage myPoultries(int oid)
        {
            try
            {
                var mp = db.Poultries.Where(x => x.user_id == oid).Select(y => new {
                    y.user_id,
                    y.pltry_name,
                    y.pltry_id,
                    y.pltry_address
                });
                if (mp!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, mp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No Poultry Found");
                }
                
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,x.Message );
            }
        }
        //-----------Add new poultry
        [HttpPost]
        public HttpResponseMessage addPoultry(Poultry p)
        {
            try
            {
                db.Poultries.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,p);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Poultry Information
        [HttpGet]
        public HttpResponseMessage poultryInfo(int pid)
        {
            try
            {
                var pi = db.Poultries.Where(x => x.pltry_id == pid).Select(s => new
                {
                    s.pltry_id,
                    s.pltry_name,
                    s.pltry_address
                });
                return Request.CreateResponse(HttpStatusCode.OK, pi);
                
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------Add woker
        [HttpPost]
        public HttpResponseMessage addWorker(Worker w)
        {
            try
            {
                db.Workers.Add(w);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, w);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-------Worker Information
        [HttpGet]
        public HttpResponseMessage workerInfo(int pltryid)
        {
            try
            {
                var q = db.Workers.Where(x => x.pltry_id == pltryid).Select(y => new
                {
                    y.worker_id,
                    y.worker_name,
                    y.worker_email,
                    y.worker_address,
                    y.worker_phoneno,
                    y.user_id,
                    y.pltry_id
                });
                return Request.CreateResponse(HttpStatusCode.OK, q);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------Broiler expenditure
        [HttpGet]
        public HttpResponseMessage expenditureInfobb(int pltryid)
        {
            try
            {
                var batch = db.BroilerBatches.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.bb_id,
                    m.bb_totalcost,
                    m.bb_totalqty,
                    m.pltry_id,
                    m.bb_name
                }).ToList();
                var batchd = db.BroilerBatchDatas.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.bb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality
                }).ToList();
                var j2 = batch.Join(batchd,
                    a => a.bb_id,
                    b => b.bb_id,
                    (a, b) => new
                    {
                        a.bb_name,
                        a.bb_totalqty,
                        a.bb_totalcost,
                        b.costoffeedinkg,
                        b.totalvaccine_cost,
                        b.totalfeedconsumedinkg,
                        b.ageindays,
                        b.mortality
                    });
                return Request.CreateResponse(HttpStatusCode.OK, j2);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------Layer expenditure
        [HttpGet]
        public HttpResponseMessage expenditureInfolb(int pltryid)
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
                    m.mortality
                }).ToList();
                var j2 = batch.Join(batchd,
                    a => a.lb_id,
                    b => b.lb_id,
                    (a, b) => new
                    {
                        a.lb_name,
                        a.lb_totalqty,
                        a.lb_totalcost,
                        b.costoffeedinkg,
                        b.totalvaccine_cost,
                        b.totalfeedconsumedinkg,
                        b.ageindays,
                        b.mortality
                    });
                return Request.CreateResponse(HttpStatusCode.OK, j2);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Put On Sales Layerbatch
        [HttpGet]
        public HttpResponseMessage lbputOnsale(int pltryid)
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
                var batchd = db.LayerBatchDatas.Where(x => x.pltry_id == pltryid && x.sale_status==0).Select(m => new {
                    m.lb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality,
                    m.sale_status,
                    m.eggs_produced
                }).ToList();
                var j2 = batch.Join(batchd,
                    a => a.lb_id,
                    b => b.lb_id,
                    (a, b) => new
                    {
                        a.lb_id,
                        a.lb_name,
                        a.lb_totalqty,
                        a.lb_totalcost,
                        b.costoffeedinkg,
                        b.totalvaccine_cost,
                        b.totalfeedconsumedinkg,
                        b.ageindays,
                        b.mortality,
                        b.sale_status,
                        b.eggs_produced,
                        b.pltry_id
                    });
                return Request.CreateResponse(HttpStatusCode.OK, j2);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------Update Sale Status 
        [HttpGet]
        public HttpResponseMessage lbSalestatusUpdate(int lbid,int pricepertray)
        {
            try
            {
                var q = db.LayerBatchDatas.FirstOrDefault(x => x.lb_id == lbid);
                q.sale_status = 1;
                db.SaveChanges();
                if (q!=null)
                {
                    var batch = db.LayerBatches.Where(x => x.pltry_id == q.pltry_id&&x.lb_id==q.lb_id).Select(m => new {
                        m.lb_id,
                        m.lb_totalcost,
                        m.lb_totalqty,
                        m.pltry_id,
                        m.lb_name
                    }).ToList();
                    var batchd = db.LayerBatchDatas.Where(x => x.pltry_id == q.pltry_id&&x.lb_id==q.lb_id).Select(m => new {
                        m.lb_id,
                        m.costoffeedinkg,
                        m.totalvaccine_cost,
                        m.pltry_id,
                        m.totalfeedconsumedinkg,
                        m.ageindays,
                        m.mortality,
                        m.sale_status,
                        m.eggs_produced
                    }).ToList();
                    var j2 = batch.Join(batchd,
                        a => a.lb_id,
                        b => b.lb_id,
                        (a, b) => new
                        {
                            a.lb_id,
                            a.lb_name,
                            a.lb_totalqty,
                            a.lb_totalcost,
                            b.costoffeedinkg,
                            b.totalvaccine_cost,
                            b.totalfeedconsumedinkg,
                            b.ageindays,
                            b.mortality,
                            b.sale_status,
                            b.eggs_produced,
                            b.pltry_id
                        }).FirstOrDefault();
                    Sale s = new Sale();
                    s.lb_id = j2.lb_id;
                    s.b_name = j2.lb_name;
                    s.lb_totaleggsintrays = j2.eggs_produced;
                    s.pltry_id = j2.pltry_id;
                    s.lb_pricepertryofeggs = pricepertray;
                    s.s_status = 0;
                    db.Sales.Add(s);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, q);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Put On Sales Broilerbatch
        [HttpGet]
        public HttpResponseMessage bbputOnsale(int pltryid)
        {
            try
            {
                var batch = db.BroilerBatches.Where(x => x.pltry_id == pltryid).Select(m => new {
                    m.bb_id,
                    m.bb_totalcost,
                    m.bb_totalqty,
                    m.pltry_id,
                    m.bb_name
                }).ToList();
                var batchd = db.BroilerBatchDatas.Where(x => x.pltry_id == pltryid && x.sale_status == 0).Select(m => new {
                    m.bb_id,
                    m.costoffeedinkg,
                    m.totalvaccine_cost,
                    m.pltry_id,
                    m.totalfeedconsumedinkg,
                    m.ageindays,
                    m.mortality,
                    m.sale_status,
                    m.avgbodyweightingrams
                }).ToList();
                var j2 = batch.Join(batchd,
                    a => a.bb_id,
                    b => b.bb_id,
                    (a, b) => new
                    {
                        a.bb_id,
                        a.bb_name,
                        a.bb_totalqty,
                        a.bb_totalcost,
                        b.costoffeedinkg,
                        b.totalvaccine_cost,
                        b.totalfeedconsumedinkg,
                        b.ageindays,
                        b.mortality,
                        b.sale_status,
                        b.avgbodyweightingrams,
                        b.pltry_id
                    });
                return Request.CreateResponse(HttpStatusCode.OK, j2);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------Update Sale Status 
        [HttpGet]
        public HttpResponseMessage bbSalestatusUpdate(int bbid,int priceperchick)
        {
            try
            {
                var q = db.BroilerBatchDatas.FirstOrDefault(x => x.bb_id == bbid);
                q.sale_status = 1;
                db.SaveChanges();
                if (q!=null)
                {
                    var batch = db.BroilerBatches.Where(x => x.pltry_id == q.pltry_id&&x.bb_id==q.bb_id).Select(m => new {
                        m.bb_id,
                        m.bb_totalcost,
                        m.bb_totalqty,
                        m.pltry_id,
                        m.bb_name
                    }).ToList();
                    var batchd = db.BroilerBatchDatas.Where(x => x.pltry_id == q.pltry_id && x.bb_id==q.bb_id).Select(m => new {
                        m.bb_id,
                        m.costoffeedinkg,
                        m.totalvaccine_cost,
                        m.pltry_id,
                        m.totalfeedconsumedinkg,
                        m.ageindays,
                        m.mortality,
                        m.sale_status,
                        m.avgbodyweightingrams
                    }).ToList();
                    var j2 = batch.Join(batchd,
                        a => a.bb_id,
                        b => b.bb_id,
                        (a, b) => new
                        {
                            a.bb_id,
                            a.bb_name,
                            a.bb_totalqty,
                            a.bb_totalcost,
                            b.costoffeedinkg,
                            b.totalvaccine_cost,
                            b.totalfeedconsumedinkg,
                            b.ageindays,
                            b.mortality,
                            b.sale_status,
                            b.avgbodyweightingrams,
                            b.pltry_id
                        }).FirstOrDefault();
                    Sale s = new Sale();
                    s.bb_id = j2.bb_id;
                    s.b_name = j2.bb_name;
                    s.bb_totalqty = j2.bb_totalqty-j2.mortality;
                    s.bb_avgbodyweight = j2.avgbodyweightingrams;
                    s.pltry_id = j2.pltry_id;
                    s.bb_priceperchick = priceperchick; 
                    s.s_status = 0;
                    db.Sales.Add(s);
                    db.SaveChanges();
                    
                }
                return Request.CreateResponse(HttpStatusCode.OK,  q);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}
