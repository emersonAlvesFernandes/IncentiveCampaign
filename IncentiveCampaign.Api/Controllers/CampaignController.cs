﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Apl;
using FastMapper;
using IncentiveCampaign.Domain.Term;
using IncentiveCampaign.Api.Models;
using IncentiveCampaign.Api.Models.IncentiveCampaign;
using IncentiveCampaign.Api.Models.Dealership;
using IncentiveCampaign.Api.Models.Term;

namespace IncentiveCampaign.Api.Controllers
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
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(List<IncentiveCampaignCreate>))]
        public async Task<IHttpActionResult> CreateAsync([FromBody]IncentiveCampaignCreate incentiveCampaignCreate)
        {
            var username = 1234;

            var incentiveCampaignEntity = new IncentiveCampaignCreate()
                .ToIncentiveCampaignEntity(incentiveCampaignCreate);            

            incentiveCampaignEntity.Dealerships = new DealershipSummary()
                .ToDealershipEntity(incentiveCampaignCreate.Dealerships);

            var entidade = 
                TypeAdapter.Adapt<IncentiveCampaignCreate, IncentiveCampaignEntity>(incentiveCampaignCreate);

            var entity = await Task.Run(() => incentiveCampaignApl.Create(incentiveCampaignEntity));

            var retorno =
                TypeAdapter.Adapt<IncentiveCampaignEntity, IncentiveCampaignCreate>(entity);

            return this.Ok(retorno);
        }

        //admin -> tela de todas as campanhas
        //**
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

            summary.Dealerships =
                TypeAdapter.Adapt<List<DealershipEntity>, List<DealershipSummary>>(entity.Dealerships);

            summary.Terms =
                TypeAdapter.Adapt<List<TermEntity>, List<TermSummary>>(entity.Terms);
            
            return this.Ok(summary);
        }

        //admin -> tela de edição decampanhas (edita somente a campanha, para os dealerships o serviço é separado)
        //**
        [HttpPut]
        [Route("{campaignId}")]
        [ResponseType(typeof(IncentiveCampaignSummary))]
        public async Task<IHttpActionResult> Update([FromBody] IncentiveCampaignCreate incentiveCampaignCreate)
        {
            var incentiveCampaignEntity = new IncentiveCampaignCreate()
                .ToIncentiveCampaignEntity(incentiveCampaignCreate);

            var entity = await Task.Run(() => incentiveCampaignApl.Update(incentiveCampaignEntity));

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
