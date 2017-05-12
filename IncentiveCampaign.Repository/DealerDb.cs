using IncentiveCampaign.Domain.Dealer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{

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

        public DealerEntity ReadCorporateDealerById(int dealerId)
        {
            throw new NotImplementedException();
        }
    }
}
