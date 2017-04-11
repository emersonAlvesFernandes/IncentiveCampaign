using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.IncentiveCampaign.ViewModels;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{       
    
    public interface IIncentiveCampaignApl
    {
        IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign);

        List<IncentiveCampaignEntity> GetAll();

        List<IncentiveCampaignEntity> GetByDealer(int dealerId);

        List<IncentiveCampaignEntity> GetByDealership(int dealershipId);

        List<IncentiveCampaignEntity> GetManagerCampaigns(int dealershipId, int managerId);

    }


    public class IncentiveCampaignApl : IIncentiveCampaignApl
    {
        private readonly IIncentiveCampaignDb incentiveCampaignDb;
        private readonly IDealershipDb dealershipDb;
        private readonly IDealerDb dealerDb;
        private readonly IScoreDb scoreDb;

        public IncentiveCampaignApl(IIncentiveCampaignDb database)
        {
            this.incentiveCampaignDb = database;
        }

        public IncentiveCampaignApl()
        {
            incentiveCampaignDb = new IncentiveCampaignDb();
            dealershipDb = new DealershipDb();
            dealerDb = new DealerDb();
            scoreDb = new ScoreDb();
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {            
            var campaign = incentiveCampaignDb.Create(incentiveCampaign);

            foreach(var d in incentiveCampaign.Dealerships)
            {
                var dealership = dealershipDb.Create(d, campaign.Id);
                campaign.Dealerships.Add(dealership);
            }
            
            return campaign;
        }
        
        public List<IncentiveCampaignEntity> GetAll()
        {
            return incentiveCampaignDb.ReadAll();
        }

        public List<IncentiveCampaignEntity> GetByDealer(int dealerId)
        {
            return incentiveCampaignDb.ReadByDealer(dealerId);
        }

        public List<IncentiveCampaignEntity> GetByDealership(int dealershipId)
        {
            return incentiveCampaignDb.ReadByDealer(dealershipId);
        }

        public List<IncentiveCampaignEntity> GetManagerCampaigns(int dealershipId, int managerId)
        {
            var campaigns = this.GetByDealership(dealershipId);            
            foreach (var campaign in campaigns)
            {
                var user = dealerDb.ReadByIdAndCampaignId(managerId, campaign.Id);

                var dealership = dealershipDb.ReadById(dealershipId);

                user.Scores = scoreDb.ReadByDealerAndDealership(dealership.Id, user.Id);

                dealership.Dealers.Add(user);

                campaign.Dealerships.Add(dealership);                                
            }

            //N Campaigns >> 1 Dealerships >> 1 Dealer >> N Scores
            return campaigns;
        }
    }
}
