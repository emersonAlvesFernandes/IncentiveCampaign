using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Term;
using IncentiveCampaign.WebApi.Models;
using IncentiveCampaign.WebApi.Models.Dealership;
using IncentiveCampaign.WebApi.Models.IncentiveCampaign;
using IncentiveCampaign.WebApi.Models.Term;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IncentiveCampaign.WebApi.Controllers
{
    [RoutePrefix("api/campaign")]
    public class CampaignController : ApiController
    {
        private readonly IIncentiveCampaignApl incentiveCampaignApl;

        public CampaignController()
        {
            incentiveCampaignApl = new IncentiveCampaignApl();
        }

        

        //admin -> criação de campanhas
        //**
        //Cria campanha, dealerships e termo(s)
        [HttpPost]
        [Route("")]
        //[ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> CreateAsync([FromBody]IncentiveCampaignCreate incentiveCampaignCreate)
        {                        
            var campaignEntity =
                TypeAdapter.Adapt<IncentiveCampaignCreate, IncentiveCampaignEntity>(incentiveCampaignCreate);

            campaignEntity.UserName = "RBRONZO";
            
            var entity = await Task.Run(() => incentiveCampaignApl.Create(campaignEntity));
            
            return this.Ok();            
        }

        //admin -> tela de todas as campanhas
        //**
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<IncentiveCampaignSummary>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var collection = await Task.Run(() => incentiveCampaignApl.GetAll());

            var returnCollection =
                TypeAdapter.Adapt<List<IncentiveCampaignEntity>, List<IncentiveCampaignSummary>>(collection);

            return this.Ok(returnCollection);
        }

        //admin -> tela de edição decampanhas
        //**
        [HttpGet]
        [Route("{campaignId}")]
        [ResponseType(typeof(IncentiveCampaignWithLists))]
        public async Task<IHttpActionResult> GetById([FromUri] int campaignId)
        {
            var entity = await Task.Run(() => incentiveCampaignApl.GetById(campaignId));

            var summary =
                TypeAdapter.Adapt<IncentiveCampaignEntity, IncentiveCampaignWithLists>(entity);
            
            return this.Ok(summary);
        }

        //admin -> tela de edição decampanhas (edita somente a campanha, para os dealerships o serviço é separado)
        //** Parei aqui: está dando null exception na camada de banco
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(IncentiveCampaignSummary))]
        public async Task<IHttpActionResult> Update([FromBody] IncentiveCampaignUpdate incentiveCampaignCreate)
        {
            //var incentiveCampaignEntity = new IncentiveCampaignCreate()
            //    .ToIncentiveCampaignEntity(incentiveCampaignCreate);

            var campaignEntity =
                TypeAdapter.Adapt<IncentiveCampaignUpdate, IncentiveCampaignEntity>(incentiveCampaignCreate);

            var entity = await Task.Run(() => incentiveCampaignApl.Update(campaignEntity));

            var summary =
                TypeAdapter.Adapt<IncentiveCampaignEntity, IncentiveCampaignSummary>(entity);

            return this.Ok(summary);
        }

        //admin -> tela de inserir pontos manuais 
        //*
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
        //*
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

        //*
        [HttpPost]
        [Route("{campaignId}/upload/term")]
        [ResponseType(typeof(List<TermEntity>))]
        public async Task<IHttpActionResult> UploadTerm([FromUri] int campaignId)
        {
            //TODO
            return this.Ok();
        }

        //bmb 
        //TODO: implementar GetUserCampaigns, que retorne IncentiveCampaignWithScoreAmmount
        // a diferença para o GetManagerCampaigns é que este leva em consideração somente as campanhas em que o 
        // usuário participa (não todas as campanhas da concessionária)

    }
}
