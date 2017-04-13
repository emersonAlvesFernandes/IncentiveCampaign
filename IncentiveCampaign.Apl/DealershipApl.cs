using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{
    public interface IDealershipApl
    {
        DealershipEntity Create(int campaignId, DealershipEntity dealership);

        bool Delete(int campaignId, int dealershipId);
    }

    public class DealershipApl : IDealershipApl
    {
        private readonly IDealershipDb dealershipDb;
        private readonly IIncentiveCampaignDb incentiveCampaignDb;

        public DealershipApl(IDealershipDb dealershipDb,
            IIncentiveCampaignDb incentiveCampaignDb)
        {
            this.dealershipDb = dealershipDb;
            this.incentiveCampaignDb = incentiveCampaignDb;
        }

        public DealershipEntity Create(int campaignId, DealershipEntity dealership)
        {
            var relatedCampaign = incentiveCampaignDb.ReadById(campaignId);

            if (relatedCampaign != null)
                return dealershipDb.Create(campaignId, dealership);
            else
                throw new Exception("Campaign.Does.Not.Exists");
        }

        public bool Delete(int campaignId, int dealershipId)
        {
            var relatedCampaign = incentiveCampaignDb.ReadById(campaignId);

            if (relatedCampaign != null)
                return dealershipDb.Delete(campaignId, dealershipId);
            else
                throw new Exception("Campaign.Does.Not.Exists");
        }
    }
}
