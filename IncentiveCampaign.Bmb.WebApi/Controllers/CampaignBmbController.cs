using IncentiveCampaign.Apl;
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
    public class CampaignBmbController : ApiController
    {
        private readonly IIncentiveCampaignApl incentiveCampaignApl;

        public CampaignBmbController()
        {
            this.incentiveCampaignApl = new IncentiveCampaignApl();
        }

        public CampaignBmbController(IIncentiveCampaignApl incentiveCampaignApl)
        {
            this.incentiveCampaignApl = incentiveCampaignApl;
        }
        
        [HttpGet]
        [Route("{dealershipId}/manager")]
        [ResponseType(typeof(List<IncentiveCampaignWithScoreAmmount>))]
        public async Task<IHttpActionResult> GetManagerCampaignsAsync([FromUri]int dealershipId)
        {
            //get user data
            var user = 123;

            //var campaignsByDealership = await Task.Run(() => incentiveCampaignApl.GetByDealership(dealershipId));
            var collection = await Task.Run(() => incentiveCampaignApl.GetManagerCampaigns(dealershipId, user));

            //TODO: Transformar collection em view model:
            // campaign >> dealerships >> dealer >> scores

            return this.Ok();
        }

        //bmb 
        //TODO: implementar GetUserCampaigns, que retorne IncentiveCampaignWithScoreAmmount
        // a diferença para o GetManagerCampaigns é que este leva em consideração somente as campanhas em que o 
        // usuário participa (não todas as campanhas da concessionária)
    }
}
