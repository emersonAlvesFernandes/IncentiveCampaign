using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Term;
using IncentiveCampaign.WebApi.Models.Term;
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
    
    public class TermController : ApiController
    {
        private readonly ITermApl termApl;

        public TermController()
        {
            this.termApl = new TermApl();
        }
        
        //*
        [HttpPost]
        [Route("{campaignId}/upload/term")]
        [ResponseType(typeof(List<TermEntity>))]
        public async Task<IHttpActionResult> UploadAsync([FromUri] int campaignId, [FromBody]TermCreate termCreate)
        {
            var termEntity =
                TypeAdapter.Adapt<TermCreate, TermEntity>(termCreate);

            await Task.Run(()=> this.termApl.Upload(campaignId, termEntity));
            
            return this.Ok();
        }

        //TODO TESTAR ESTE FLUXO
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(TermEntity))]
        public async Task<IHttpActionResult> DownloadAsync([FromUri] int id)
        {            
            var term = await Task.Run(()=> this.termApl.Download(id));

            return this.Ok(term);
        }
    }
}
