using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.AppService;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.WebApi.Models;
using IncentiveCampaign.WebApi.Models.IncentiveCampaign;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IncentiveCampaign.WebApi.Controllers
{
    [RoutePrefix("api/incentivecampaigns")]
    public class CampaignController : ApiController
    {
        private readonly IIncentiveCampaignAppService incentiveCampaignAppService;

        public CampaignController()
        {
            incentiveCampaignAppService = new IncentiveCampaignAppService();
        }

        public CampaignController(IIncentiveCampaignAppService incentiveCampaignAppService)
        {
            this.incentiveCampaignAppService = incentiveCampaignAppService;
        }

        //TESTADO OK
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
            
            var entity = await Task.Run(() => incentiveCampaignAppService.Create(campaignEntity));
            
            return this.Ok();            
        }
        
        //TESTADO OK
        //admin -> tela de todas as campanhas
        //**
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<IncentiveCampaignSummary>))]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var collection = await Task.Run(() => incentiveCampaignAppService.GetAll());

            var returnCollection =
                TypeAdapter.Adapt<List<IncentiveCampaignEntity>, List<IncentiveCampaignSummary>>(collection);

            return this.Ok(returnCollection);
        }

        //TESTADO OK
        //admin -> tela de edição decampanhas
        //**
        [HttpGet]
        [Route("{campaignId}")]
        [ResponseType(typeof(IncentiveCampaignWithLists))]
        public async Task<IHttpActionResult> GetByIdAsync([FromUri] int campaignId)
        {
            var entity = await Task.Run(() => incentiveCampaignAppService.GetById(campaignId));

            var summary =
                TypeAdapter.Adapt<IncentiveCampaignEntity, IncentiveCampaignWithLists>(entity);
            
            return this.Ok(summary);
        }

        //TESTADO OK >> TESTADO O CONTEXTO DE DEALERSHIP > TESTAR O CONTEXTO DO TERMO
        //admin -> tela de edição decampanhas (edita somente a campanha, para os dealerships o serviço é separado)
        //** 
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(IncentiveCampaignSummary))]
        public async Task<IHttpActionResult> UpdateAsync([FromBody] IncentiveCampaignUpdate incentiveCampaignCreate)
        {            
            var campaignEntity =
                TypeAdapter.Adapt<IncentiveCampaignUpdate, IncentiveCampaignEntity>(incentiveCampaignCreate);

            var entity = await Task.Run(() => incentiveCampaignAppService.Update(campaignEntity));

            var summary =
                TypeAdapter.Adapt<IncentiveCampaignEntity, IncentiveCampaignSummary>(entity);

            return this.Ok(summary);
        }


        //admin -> tela de inserir pontos manuais 
        //*
        [HttpGet]
        [Route("dealer/{dealerId}")]
        [ResponseType(typeof(List<IncentiveCampaignSummary>))]
        public async Task<IHttpActionResult> GetByDealerIdAsync([FromUri]int dealerId)
        {
            var collection = await Task.Run(() => incentiveCampaignAppService.GetByDealer(dealerId));

            var returnCollection =
                TypeAdapter.Adapt<List<IncentiveCampaignEntity>, List<IncentiveCampaignSummary>>(collection);

            return this.Ok(returnCollection);
        }
       
    }
}
