using IncentiveCampaign.CorporateRepository;
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
        bool Register(int campaignId, DealershipEntity dealership);

        bool Delete(int campaignId, int dealershipId);

        List<DealershipEntity> GetByCampaign(int campaignId);

        List<DealershipEntity> GetByDealer(int dealerId);

        DealershipEntity GetById(int dealershipId);
    }

    public class DealershipApl : IDealershipApl
    {
        private readonly IDealershipDb dealershipDb;        
        private readonly IIncentiveCampaignApl incentiveCampaignApl;

        public DealershipApl(IDealershipDb dealershipDb,
            IIncentiveCampaignApl incentiveCampaignApl)
        {
            this.dealershipDb = dealershipDb;
            this.incentiveCampaignApl = incentiveCampaignApl;
        }

        public DealershipApl()
        {
            this.dealershipDb = new DealershipCorporateDb();
        }

        public bool Register(int campaignId, DealershipEntity dealership)
        {            
            var relatedCampaign = incentiveCampaignApl.GetById(campaignId);

            var dealershipsOnCampaign = dealershipDb.ReadByCampaign(campaignId);

            if (dealershipsOnCampaign.Contains(dealership))
                throw new Exception("Dealership.Already.Registered");

            if (relatedCampaign == null)
                throw new Exception("Campaign.Does.Not.Exists");
            
            return dealershipDb.Register(campaignId, dealership);                            
        }

        public bool Delete(int campaignId, int dealershipId)
        {
            var relatedCampaign = incentiveCampaignApl.GetById(campaignId);

            if (relatedCampaign == null)
                throw new Exception("Campaign.Does.Not.Exists");
            
            return dealershipDb.Delete(campaignId, dealershipId);                            
        }

        public List<DealershipEntity> GetByCampaign(int campaignId)
        {
            return this.dealershipDb.ReadByCampaign(campaignId);
        }

        public List<DealershipEntity> GetByDealer(int dealerId)
        {
            return this.dealershipDb.ReadByDealer(dealerId);
        }

        public DealershipEntity GetById(int dealershipId)
        {
            return dealershipDb.ReadById(dealershipId);
        }
    }
}
