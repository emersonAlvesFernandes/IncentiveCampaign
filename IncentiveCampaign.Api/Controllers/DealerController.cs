using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Dealer;
using IncentiveCampaign.Domain.Dealer.ViewModel;
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
    public class DealerController : ApiController
    {

        private readonly IDealerApl dealerApl;
        
        public DealerController()
        {
            dealerApl = new DealerApl();
        }
        
        // admin -> relação de deales com total de pontos 
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<DealerWithScoreAmmount>))]
        public async Task<IHttpActionResult> GetAllDealersWithScoreAmmount()
        {
            var collection = await Task.Run(() => dealerApl.GetAll());

            var dealersWithScoreAmmount = new DealerWithScoreAmmount().ToDealerWithScoreAmmount(collection);

            return this.Ok(dealersWithScoreAmmount);
        }

        //bmb -> relação de dealer por concessionária, tela de relacionamento de dealers para a campanha
        [HttpGet]
        [Route("dealership/{dealershipid}")]
        [ResponseType(typeof(List<DealerSummary>))]
        public async Task<IHttpActionResult> GetDealersByDealership([FromUri] int dealershipId)
        {
            var collection = await Task.Run(() => dealerApl.GeByDealership(dealershipId));

            var returnCollection =
                TypeAdapter.Adapt<List<Dealer>, List<DealerSummary>>(collection);

            return this.Ok(returnCollection);
        }

        //bmb -> relacionar dealers com concessionária
        [HttpPost]
        [Route("register")]
        [ResponseType(typeof(IDictionary<int, string>))]
        public async Task<IHttpActionResult> RegisterDealerToCampaign([FromBody] DealerIds postParam)
        {
            var collection = await Task.Run(() => dealerApl.RegisterToCampaign(postParam.Ids, postParam.CampaignId));
            
            return this.Ok();
        }

    }
}
