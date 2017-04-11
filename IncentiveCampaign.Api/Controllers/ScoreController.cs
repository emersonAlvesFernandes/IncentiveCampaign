using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Score;
using IncentiveCampaign.Domain.Score.RequestModel;
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
    public class ScoreController : ApiController
    {
        private readonly IScoreApl scoreApl;

        public ScoreController()
        {
            scoreApl = new ScoreApl();
        }
            
        //admin / bmb (extrato com pontos e baixas)
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(List<Score>))]
        public async Task<IHttpActionResult> GetAllByDealerId([FromBody] PeriodScore score)
        {
            var collection = await Task.Run(()=> scoreApl.GetByDealer(score.DealerId, score.StartDate, score.EndDate));
            
            return this.Ok(collection);
        }

        //admin
        [HttpPost]
        [Route("create")]
        [ResponseType(typeof(Score))]
        public async Task<IHttpActionResult> CreateScore([FromBody] Score score)
        {
            var collection = await Task.Run(() => scoreApl.CreateScore(score));

            return this.Ok(collection);
        }

        //admin
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Score>))]
        public async Task<IHttpActionResult> GetValidByDealerId([FromUri] int dealerId)
        {
            var collection = await Task.Run(() => scoreApl.GetOnlyValid(dealerId));

            return this.Ok(collection);
        }

        //admin
        [HttpPost]
        [Route("writedown")]
        [ResponseType(typeof(IDictionary<int, string>))]
        public async Task<IHttpActionResult> WriteDownScores([FromBody]ScoresIds scoresIds)
        {
            //TODO: Resolver erro: estou criando ponto total abaixo, mas não está agrupado por campanha

            var collection = await Task.Run(() => scoreApl.WriteDown(scoresIds.Ids));

            return this.Ok(collection);
        }

        //admin
        [HttpPost]
        [Route("dealers/writedown")]
        [ResponseType(typeof(List<Score>))]
        public async Task<IHttpActionResult> WriteDownScoresByDealers([FromBody]ScoresIds scoresIds)
        {
            //TODO
            //Fazer método de criação de pontos via importação de planilha    
            return this.Ok();
        }

        
    }
}
