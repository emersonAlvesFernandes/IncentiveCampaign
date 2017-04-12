using FastMapper;
using IncentiveCampaign.Api.Models.Dealership;
using IncentiveCampaign.Api.Models.IncentiveCampaign;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Dealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IncentiveCampaign.Api.Controllers
{
    public class DealershipController : ApiController
    {
        private readonly IDealershipApl dealershipApl;

        public DealershipController(IDealershipApl dealershipApl)
        {
            this.dealershipApl = dealershipApl;
        }

        [HttpPost]
        [Route("{campaignId}")]
        [ResponseType(typeof(DealershipCreate))]
        public async Task<IHttpActionResult> Create([FromBody] DealershipCreate dealershipCreate)
        {
            var dealershipEntity = new DealershipCreate().ToDealershipEntity(dealershipCreate);

            var entity = await Task.Run(() => dealershipApl.Create(dealershipCreate.Id,
                dealershipEntity));

            var summary =
                TypeAdapter.Adapt<DealershipEntity, DealershipCreate>(entity);
            
            return this.Ok(summary);
        }
    }
}
