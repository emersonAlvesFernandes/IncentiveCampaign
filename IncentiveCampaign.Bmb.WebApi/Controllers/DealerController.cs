﻿using IncentiveCampaign.Apl;
using IncentiveCampaign.Bmb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IncentiveCampaign.Bmb.WebApi.Controllers
{
    [RoutePrefix("api/userincentivecampaigns")]
    public class DealerController : ApiController
    {
        private readonly IDealerApl dealerApl;

        public DealerController()
        {
            dealerApl = new DealerApl();
        }

        //bmb -> relação de dealer por concessionária, tela de relacionamento de dealers para a campanha
        [HttpGet]
        [Route("dealership/{dealershipid}")]
        [ResponseType(typeof(List<DealerSummary>))]
        public async Task<IHttpActionResult> GetDealersByDealershipAsync([FromUri] int dealershipId)
        {
            var collection = await Task.Run(() => dealerApl.GeByDealership(dealershipId));

            var returnCollection =
                TypeAdapter.Adapt<List<DealerEntity>, List<DealerSummary>>(collection);

            return this.Ok(returnCollection);
        }

        //bmb -> relacionar dealers com concessionária
        [HttpPost]
        [Route("register")]
        [ResponseType(typeof(IDictionary<int, string>))]
        public async Task<IHttpActionResult> RegisterDealerToCampaignAsync([FromBody] DealerIds postParam)
        {
            var collection = await Task.Run(() => dealerApl.RegisterToCampaign(postParam.Ids, postParam.CampaignId));

            return this.Ok();
        }
    }
}
