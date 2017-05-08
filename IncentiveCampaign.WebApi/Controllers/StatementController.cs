using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Statement;
using IncentiveCampaign.WebApi.Models.Dealer;
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
    [RoutePrefix("api/dealer")]
    public class StatementController : ApiController
    {
        private readonly IStatementApl statementApl;

        public StatementController()
        {
            statementApl = new StatementApl();
        }

        public StatementController(IStatementApl statementApl)
        {
            this.statementApl = statementApl;
        }
        

        // admin -> relação de deales com total de pontos 
        [HttpGet]
        [Route("statement")]
        [ResponseType(typeof(List<StatementItemRow>))]
        public async Task<IHttpActionResult> GetAllDealersWithScoreAmmountAsync()
        {
            var collection = await Task.Run(()=> statementApl.GetStatement());
            
            return this.Ok(collection);
        }
    }
}
