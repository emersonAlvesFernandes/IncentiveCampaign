using IncentiveCampaign.Domain.Dealer;
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
    }

    public class DealerApl : IDealerApl
    {
        private readonly IDealerDb dealerDb;
        private readonly IScoreDb scoreDb;
        private readonly IDealershipDb dealershipDb;
        private readonly IIncentiveCampaignDb campaignDb;

        public DealerApl()
        {
            this.dealerDb = new DealerDb();
            this.scoreDb = new ScoreDb();
        }

        public DealerApl(IDealerDb dealerDb, IScoreDb scoreDb)
        {
            this.dealerDb = dealerDb;
            this.scoreDb = scoreDb;
        }

        public List<DealerEntity> GetAll()
        {
            var collection = dealerDb.ReadAll();

            foreach(var c in collection)
            {
                c.Scores = scoreDb.ReadByDealer(c.Id);
                c.Dealerships = dealershipDb.ReadByDealer(c.Id);
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
            var campaign = campaignDb.ReadById(campaignId);
            if(campaign == null)            
                throw new Exception("campaign.not.found");
            
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

                if(isSucceed)
                    collection.Add(id, "ok");
                else                
                    collection.Add(id, "error.to.register");                
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
    }
}
