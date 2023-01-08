using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoultryFarmManagementSystem.Models;

namespace PoultryFarmManagementSystem.Controllers
{
    public class WorkerController : ApiController
    {
        PoultryFarmManagementSystemEntities10 db = new PoultryFarmManagementSystemEntities10();
        //--Add Broiler Batch
        [HttpPost]
        public HttpResponseMessage addBroilerBatch(BroilerBatch bb)
        {
            try
            {
                db.BroilerBatches.Add(bb);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, bb);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--Add Layer Batch
        [HttpPost]
        public HttpResponseMessage addLayerBatch(LayerBatch lb)
        {
            try
            {
                db.LayerBatches.Add(lb);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, lb);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--Getting Layer Batches
        [HttpGet]
        public HttpResponseMessage LayerBatches(int wid)
        {
            try
            {
                var lb = db.LayerBatches.Where(m => m.worker_id == wid).Select(s => new
                {
                    s.lb_arrivaldate,
                    s.lb_id,
                    s.lb_name,
                    s.lb_totalcost,
                    s.lb_totalqty,
                    s.worker_id,
                    s.pltry_id
                });
                return Request.CreateResponse(HttpStatusCode.OK, lb);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--Getting Broiler Batches
        [HttpGet]
        public HttpResponseMessage broilerBatches(int wid)
        {
            try
            {

                var bb = db.BroilerBatches.Where(m => m.worker_id == wid).Select(s => new
                {
                    s.bb_arrivaldate,
                    s.bb_id,
                    s.bb_name,
                    s.bb_totalcost,
                    s.bb_totalqty,
                    s.worker_id,
                    s.pltry_id
                });
                return Request.CreateResponse(HttpStatusCode.OK, bb);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Add broiler batch data
        [HttpPost]
        public HttpResponseMessage addBroilerData(BroilerBatchData bbdata)
        {
            try
            {
                //List<BroilerBatchData> results = (from m in db.BroilerBatchDatas
                //                                 where m.bb_id == bbdata.bb_id
                //                                 select m).ToList();
                var getbdata = db.BroilerBatchDatas.FirstOrDefault(x => x.bb_id ==bbdata.bb_id);
                List<BroilerBatchData> bd = new List<BroilerBatchData>();
                bd.Add(getbdata);
                if (bd[0] == null)
                {
                    db.BroilerBatchDatas.Add(bbdata);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, bbdata);
                }
                else
                {
                    getbdata.feed_type = bbdata.feed_type;
                    getbdata.costoffeedinkg += bbdata.costoffeedinkg;
                    getbdata.vaccine_name = bbdata.vaccine_name;
                    getbdata.totalvaccine_cost += bbdata.totalvaccine_cost;
                    getbdata.totalfeedconsumedinkg += bbdata.totalfeedconsumedinkg;
                    getbdata.avgbodyweightingrams = bbdata.avgbodyweightingrams;
                    getbdata.ageindays = bbdata.ageindays;
                    getbdata.mortality += bbdata.mortality;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, getbdata);
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Get Broiler Batch Info
        [HttpGet]
        public HttpResponseMessage broilerbatchInfo(int bbid)
        {
            try
            {
                var bbdata = db.BroilerBatches.Where(x=>x.bb_id==bbid).Select(m=>new {
                    m.bb_id,
                    m.bb_arrivaldate,
                    m.bb_name,
                    m.bb_totalcost,
                    m.bb_totalqty,
                    m.pltry_id,
                    m.worker_id
                });
                return Request.CreateResponse(HttpStatusCode.OK, bbdata);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
                throw;
            }
        }
        //---------------
        //-----Add layer batch data
        [HttpPost]
        public HttpResponseMessage addLayerData(LayerBatchData lbdata)
        {
            try
            {
                //List<BroilerBatchData> results = (from m in db.BroilerBatchDatas
                //                                 where m.bb_id == bbdata.bb_id
                //                                 select m).ToList();
                var getldata = db.LayerBatchDatas.FirstOrDefault(x => x.lb_id == lbdata.lb_id);
                List<LayerBatchData> ld = new List<LayerBatchData>();
                ld.Add(getldata);
                if (ld[0] == null)
                {
                    db.LayerBatchDatas.Add(lbdata);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, lbdata);
                }
                else
                {
                    getldata.feed_type = lbdata.feed_type;
                    getldata.costoffeedinkg += lbdata.costoffeedinkg;
                    getldata.vaccine_name = lbdata.vaccine_name;
                    getldata.totalvaccine_cost += lbdata.totalvaccine_cost;
                    getldata.totalfeedconsumedinkg += lbdata.totalfeedconsumedinkg;
                    getldata.eggs_produced += lbdata.eggs_produced;
                    getldata.ageindays = lbdata.ageindays;
                    getldata.mortality += lbdata.mortality;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, getldata);
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Get Layer Batch Info
        [HttpGet]
        public HttpResponseMessage layerBatchInfo(int lbid)
        {
            try
            {
                var lbdata = db.LayerBatches.Where(x => x.lb_id == lbid).Select(m => new {
                    m.lb_id,
                    m.lb_arrivaldate,
                    m.lb_name,
                    m.lb_totalcost,
                    m.lb_totalqty,
                    m.pltry_id,
                    m.worker_id
                });
                return Request.CreateResponse(HttpStatusCode.OK, lbdata);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Broiler Batch Data Info
        [HttpGet]
        public HttpResponseMessage broilerBatchDataInfo(int bbid)
        {
            try
            {
                var di = db.BroilerBatchDatas.Where(x => x.bb_id == bbid).Select(b=>new {
                    b.bb_id,
                    b.bbd_id,
                    b.feed_type,
                    b.costoffeedinkg,
                    b.vaccine_name,
                    b.totalvaccine_cost,
                    b.totalfeedconsumedinkg,
                    b.avgbodyweightingrams,
                    b.ageindays,
                    b.pltry_id,
                    b.mortality,
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, di);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Layer Batch Data Info
        [HttpGet]
        public HttpResponseMessage layerBatchDataInfo(int lbid)
        {
            try
            {
                var di = db.LayerBatchDatas.Where(x => x.lb_id == lbid).Select(b => new {
                    b.lb_id,
                    b.lbd_id,
                    b.feed_type,
                    b.costoffeedinkg,
                    b.vaccine_name,
                    b.totalvaccine_cost,
                    b.totalfeedconsumedinkg,
                    b.eggs_produced,
                    b.ageindays,
                    b.pltry_id,
                    b.mortality,
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, di);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}
