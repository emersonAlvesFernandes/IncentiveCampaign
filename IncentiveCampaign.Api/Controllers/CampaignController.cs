using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IncentiveCampaign.Domain.IncentiveCampaign.ViewModels;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Apl;
using FastMapper;

namespace IncentiveCampaign.Api.Controllers
{
    //[RoutePrefix("api/campaign")]
    public class CampaignController : ApiController
    {
        private readonly IIncentiveCampaignApl incentiveCampaignApl;

        public CampaignController()
        {
            incentiveCampaignApl = new IncentiveCampaignApl();
        }

        //admin -> criação de campanhas
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(List<IncentiveCampaignCreate>))]
        public async Task<IHttpActionResult> CreateAsync([FromBody]IncentiveCampaignCreate incentiveCampaignCreate)
        {
            var username = 1234;

            var incentiveCampaign = new IncentiveCampaignEntity()
                .ToIncentiveCampaign(incentiveCampaignCreate);

            incentiveCampaign.Dealerships = new Dealership()
                .ToDealership(incentiveCampaignCreate.Dealerships);

            var entidade = 
                TypeAdapter.Adapt<IncentiveCampaignCreate, IncentiveCampaignEntity>(incentiveCampaignCreate);

            var entity = await Task.Run(() => incentiveCampaignApl.Create(incentiveCampaign));

            var retorno =
                TypeAdapter.Adapt<IncentiveCampaignEntity, IncentiveCampaignCreate>(entity);

            return this.Ok(retorno);
        }

        //admin -> tela de todas as campanhas
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<IncentiveCampaignSummary>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var collection = await Task.Run(()=>incentiveCampaignApl.GetAll());
            
            var returnCollection =
                TypeAdapter.Adapt<List<IncentiveCampaignEntity>, List<IncentiveCampaignSummary>>(collection);

            return this.Ok(returnCollection);
        }

        //admin -> tela de inserir pontos manuais 
        [HttpGet]
        [Route("dealer/{dealerId}")]
        [ResponseType(typeof(List<IncentiveCampaignSummary>))]
        public async Task<IHttpActionResult> GetByDealerId([FromUri]int dealerId)
        {
            var collection = await Task.Run(() => incentiveCampaignApl.GetByDealer(dealerId));

            var returnCollection =
                TypeAdapter.Adapt<List<IncentiveCampaignEntity>, List<IncentiveCampaignSummary>>(collection);

            return this.Ok(returnCollection);
        }

        //bmb -> tela do bmb gestor, todas as campanhas para a concessionária
        [HttpGet]
        [Route("{dealershipId}/manager")]
        [ResponseType(typeof(List<IncentiveCampaignWithScoreAmmount>))]
        public async Task<IHttpActionResult> GetManagerCampaigns([FromUri]int dealershipId)
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
