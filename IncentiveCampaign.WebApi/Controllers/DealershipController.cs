﻿using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.WebApi.Models.Dealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IncentiveCampaign.WebApi.Controllers
{
    [RoutePrefix("api/dealership")]
    public class DealershipController : ApiController
    {
        private readonly IDealershipApl dealershipApl;

        public DealershipController()
        {
            this.dealershipApl = new DealershipApl();
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Create([FromBody] DealershipCreate dealershipCreate, 
            [FromUri] int campaignId)
        {
            //var dealershipEntity = new DealershipCreate().ToDealershipEntity(dealershipCreate);
            var dealershipEntity =
                TypeAdapter.Adapt<DealershipCreate, DealershipEntity>(dealershipCreate);

            var registerSucceed = await Task.Run(() => dealershipApl.Register(campaignId,
                dealershipEntity));
            
            return this.Ok(registerSucceed);
        }

        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Delete([FromUri]int campaignId,
    [FromUri]int dealershipId)
        {
            
            var registerSucceed = await Task.Run(() => dealershipApl.Delete(campaignId,
                dealershipId));

            return this.Ok(registerSucceed);
        }
    }
}
