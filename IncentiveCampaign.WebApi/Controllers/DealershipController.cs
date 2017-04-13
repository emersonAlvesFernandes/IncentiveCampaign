using FastMapper;
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

        public DealershipController(IDealershipApl dealershipApl)
        {
            this.dealershipApl = dealershipApl;
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(DealershipCreate))]
        public async Task<IHttpActionResult> Create([FromBody] DealershipCreate dealershipCreate)
        {
            var dealershipEntity = new DealershipCreate().ToDealershipEntity(dealershipCreate);

            var entity = await Task.Run(() => dealershipApl.Create(dealershipCreate.CampaignId,
                dealershipEntity));

            var summary =
                TypeAdapter.Adapt<DealershipEntity, DealershipCreate>(entity);

            return this.Ok(summary);
        }
    }
}
