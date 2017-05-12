using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Term;
using IncentiveCampaign.Repository;
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

        bool Register(int incentiveCampaignId, TermEntity term, string codUser);

        List<TermEntity> GetByCampaign(int campaignId);
    }

    public class TermApl : ITermApl
    {
        private readonly IIncentiveCampaignApl incentiveCampaignApl;
        private readonly ITermDb termDb;

        public TermApl()
        {
            this.incentiveCampaignApl = new IncentiveCampaignApl();
            this.termDb = new TermDb();
        }

        public bool Delete(int termId)
        {
            return termDb.Delete(termId);
        }

        public TermEntity Download(int termId)
        {
            return termDb.Download(termId);
        }

        public bool Upload(int campaignId, TermEntity term)
        {
            var relatedCampaign = incentiveCampaignApl.GetById(campaignId);

            if (relatedCampaign == null)
                throw new Exception("Campaign.Does.Not.Exists");
            
            return termDb.Upload(campaignId, term);                            
        }

        public bool Register(int incentiveCampaignId, TermEntity term, string codUser)
        {
            return termDb.Register(incentiveCampaignId, term, codUser);
        }

        public List<TermEntity> GetByCampaign(int campaignId)
        {
            return termDb.ReadByCampaign(campaignId);
        }
    }
}
