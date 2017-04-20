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
        // admin -> relação de deales com total de pontos 
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<DealerWithScoreAmmount>))]
        public async Task<IHttpActionResult> GetAllDealersWithScoreAmmount()
        {

            
        }
    }
}
