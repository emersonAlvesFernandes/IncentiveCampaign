using IncentiveCampaign.Domain.Dealer;
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
    public interface IDealerApl
    {
        List<DealerEntity> GetAll();

        List<DealerEntity> GeByDealership(int dealershipId);

        IDictionary<int, string> RegisterToCampaign(List<int> ids, int campaignId);

        DealerEntity GetByIdAndCampaignId(int dealerId, int campaignId);
    }

    public class DealerApl : IDealerApl
    {
        private readonly IDealerDb dealerDb;
        //private readonly IScoreDb scoreDb;
        //private readonly IDealershipDb dealershipDb;
        //private readonly IIncentiveCampaignDb campaignDb;
        
        private readonly IScoreApl scoreApl;
        private readonly IDealershipApl dealershipApl;
        private readonly IIncentiveCampaignApl campaignApl;

        public DealerApl()
        {
            this.dealerDb = new DealerDb();
            this.scoreApl = new ScoreApl();
            this.dealershipApl = new DealershipApl();
            this.campaignApl = new IncentiveCampaignApl();
        }

        public DealerApl(IDealerDb dealerDb,
            IDealershipApl dealershipApl,
            IIncentiveCampaignApl campaignApl            
            )
        {
            this.dealerDb = dealerDb;
            this.scoreApl = new ScoreApl();
            this.campaignApl = new IncentiveCampaignApl();
        }

        public List<DealerEntity> GetAll()
        {
            var collection = dealerDb.ReadAll();

            foreach(var c in collection)
            {
                c.Scores = scoreApl.GetByDealer(c.Id);
                c.Dealerships = dealershipApl.GetByDealer(c.Id);
            }

            return collection;
        }

        public List<DealerEntity> GeByDealership(int dealershipId)
        {
            var collection = dealerDb.ReadByDealerShip(dealershipId);
            return collection;
        }

        public IDictionary<int, string> RegisterToCampaign(List<int> ids, int campaignId)
        {
            var campaign = campaignApl.GetById(campaignId);

            if (campaign == null)            
                throw new Exception("Campaign.Not.Found");
            
            var collection = new Dictionary<int, string>();

            foreach(var id in ids)
            {
                var dealer = this.ReadOnCorporateBase(id);

                if (dealer == null)
                {
                    collection.Add(id, "dealer.not.found");
                    continue;
                }

                var isSucceed = this.RegisterToCampaign(campaignId, id);

                if (!isSucceed)
                {
                    collection.Add(id, "error.to.register");
                    continue;
                }

                collection.Add(id, "ok");
                                    
            }
            
            return collection;
        }

        private DealerEntity ReadOnCorporateBase(int id)
        {
            try
            {
                return dealerDb.ReadOnCorporateBase(id);
            }
            catch(Exception ex)
            {
                return null;
            }            
        }

        private bool RegisterToCampaign(int campaignId, int id)
        {
            try
            {
                return dealerDb.RegisterToCampaign(campaignId, id);
            }
            catch(Exception ex)
            {
                return false;
            }            
        }

        public DealerEntity GetByIdAndCampaignId(int dealerId, int campaignId)
        {
            return dealerDb.ReadByIdAndCampaignId(dealerId, campaignId);
        }

    }
}
