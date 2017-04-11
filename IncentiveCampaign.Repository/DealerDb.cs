using IncentiveCampaign.Domain.Dealer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public interface IDealerDb
    {
        List<Dealer> ReadAll();

        Dealer ReadOnCorporateBase(int userId);

        List<Dealer> ReadByDealerShip(int dealership);

        Dealer ReadByIdAndCampaignId(int dealerId, int campaignId);        

        bool RegisterToCampaign(int campaignId, int dealerId);        
    }

    public class DealerDb : IDealerDb
    {
        public Dealer ReadOnCorporateBase(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Dealer> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<Dealer> ReadByDealerShip(int dealership)
        {
            throw new NotImplementedException();
        }

        public bool RegisterToCampaign(int campaignId, int dealerId)
        {
            return true;
        }

        public Dealer ReadByIdAndCampaignId(int dealerId, int campaignId)
        {
            throw new NotImplementedException();
        }
    }
}
