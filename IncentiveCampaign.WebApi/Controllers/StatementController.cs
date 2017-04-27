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

        //TODO: Criar construtor, camada de apl e repositorio

        // admin -> relação de deales com total de pontos 
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<DealerWithScoreAmmount>))]
        public async Task<IHttpActionResult> GetAllDealersWithScoreAmmountAsync()
        {
            //TODO
            return this.Ok();
        }
    }
}
