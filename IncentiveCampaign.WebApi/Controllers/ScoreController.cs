using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Score;
using IncentiveCampaign.Domain.Statement;
using IncentiveCampaign.WebApi.Models.Score;
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
    [RoutePrefix("api/score")]
    public class ScoreController : ApiController
    {
        private readonly IScoreApl scoreApl;

        public ScoreController()
        {
            scoreApl = new ScoreApl();
        }

        //bmb (history) TODO: Implementar o mesmo na api do bmb
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(List<ScoreEntity>))]
        public async Task<IHttpActionResult> GetAllByDealerId([FromBody] PeriodScore score)
        {            
            var collection = await Task.Run(() => scoreApl.GetByDealer(score.DealerId, score.StartDate, score.EndDate));

            return this.Ok(collection);
        }

        //admin tela de relação de pontos (ao clicar no botão de baixa) criar ponto manualmente
        //(history)
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<ScoreEntity>))]
        public async Task<IHttpActionResult> GetValidByDealerId([FromUri] int dealerId)
        {
            var collection = await Task.Run(() => scoreApl.GetOnlyValid(dealerId));

            return this.Ok(collection);
        }

        //admin tela de relação de pontos (ao clicar no botão de baixa) criar ponto manualmente
        [HttpPost]
        [Route("create")]
        [ResponseType(typeof(ScoreEntity))]
        public async Task<IHttpActionResult> CreateScore([FromBody] ScoreCreate scoreCreate)
        {

            var scoreEntity =                 
                TypeAdapter.Adapt<ScoreCreate, ScoreEntity>(scoreCreate);

            var collection = await Task.Run(() => scoreApl.CreateScore(scoreCreate.CampaignId,
                scoreCreate.DealershipId,
                scoreCreate.DealerId,
                scoreEntity));

            return this.Ok(collection);
        }        

        //admin tela de relação de pontos (ao clicar no botão de baixa) criar ponto manualmente
        [HttpPost]
        [Route("writedown")]
        [ResponseType(typeof(IDictionary<int, string>))]
        public async Task<IHttpActionResult> WriteDownScores([FromBody]ScoresIds scoresIds)
        {
            //TODO: Resolver erro: estou criando ponto total abaixo, mas não está agrupado por campanha

            var collection = await Task.Run(() => scoreApl.WriteDown(scoresIds.Ids));

            return this.Ok(collection);
        }

        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> DeleteAsync([FromUri] int id)
        {
            return this.Ok();
        }



    }
}
