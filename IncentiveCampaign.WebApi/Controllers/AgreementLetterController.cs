using FastMapper;
using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.Dealer;
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
    [RoutePrefix("api/incentivecampaigns/agreementletter")]
    public class AgreementLetterController : ApiController
    {
        private readonly IDealershipApl dealershipApl;

        [HttpPost]
        [Route("{dealershipId}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> CreateAsync([FromUri] int dealershipId, [FromBody] AgreementLetterCreate agreementLetterCreate)
        {
            var agreementLetterEntity =
                TypeAdapter.Adapt<AgreementLetterCreate, AgreementLetter>(agreementLetterCreate);

            var isUploaded = dealershipApl.UploadAgreementLetter(dealershipId, agreementLetterEntity);

            return this.Ok(isUploaded);
        }
    }
}
