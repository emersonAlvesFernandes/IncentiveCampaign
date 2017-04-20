using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{
    public interface ITermApl
    {
        bool Upload(int campaignId, TermEntity term);

        TermEntity Download(int termId);        

        bool Delete(int termId);
    }

    public class TermApl : ITermApl
    {
        private readonly IIncentiveCampaignDb incentiveCampaignDb;
        private readonly ITermDb termDb;

        public TermApl()
        {

        }

        public bool Delete(int termId)
        {
            throw new NotImplementedException();
        }

        public TermEntity Download(int termId)
        {
            return termDb.Download(termId);
        }

        public bool Upload(int campaignId, TermEntity term)
        {
            var relatedCampaign = incentiveCampaignDb.ReadById(campaignId);

            if (relatedCampaign != null)
                return termDb.Upload(campaignId, term);
            else
                throw new Exception("Campaign.Does.Not.Exists");
        }
    }
}
