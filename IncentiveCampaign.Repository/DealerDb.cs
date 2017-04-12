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
        List<DealerEntity> ReadAll();

        DealerEntity ReadOnCorporateBase(int userId);

        List<DealerEntity> ReadByDealerShip(int dealership);

        DealerEntity ReadByIdAndCampaignId(int dealerId, int campaignId);        

        bool RegisterToCampaign(int campaignId, int dealerId);        
    }

    public class DealerDb : IDealerDb
    {
        public DealerEntity ReadOnCorporateBase(int userId)
        {
            throw new NotImplementedException();
        }

        public List<DealerEntity> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<DealerEntity> ReadByDealerShip(int dealership)
        {
            throw new NotImplementedException();
        }

        public bool RegisterToCampaign(int campaignId, int dealerId)
        {
            return true;
        }

        public DealerEntity ReadByIdAndCampaignId(int dealerId, int campaignId)
        {
            throw new NotImplementedException();
        }
    }
}
