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

        public AgreementLetterController()
        {
            dealershipApl = new DealershipApl();
        }

        public AgreementLetterController(IDealershipApl dealershipApl)
        {
            this.dealershipApl = dealershipApl;
        }


        [HttpPost]
        [Route("{dealershipId}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> CreateAsync([FromBody] AgreementLetterCreate agreementLetterCreate)
        {
            var agreementLetterEntity =
                TypeAdapter.Adapt<AgreementLetterCreate, AgreementLetter>(agreementLetterCreate);

            var isUploaded = await Task.Run(() => dealershipApl.UploadAgreementLetter(agreementLetterCreate.CampaignId,
                agreementLetterCreate.DealershipId,
                agreementLetterEntity));

            return this.Ok(isUploaded);
        }
    }
}
