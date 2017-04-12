﻿using IncentiveCampaign.Domain.Dealership;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{

    public interface IDealershipDb
    {
        DealershipEntity Create(int campaignId, DealershipEntity incentiveCampaign);

        bool Delete( int campaignId, int dealershipId);

        DealershipEntity ReadById(int dealershipId);

        List<DealershipEntity> ReadByCampaign(int campaign);

        List<DealershipEntity> ReadByDealer(int dealerId);
    }

    public class DealershipDb : IDealershipDb
    {
        public DealershipEntity Create(int campaignId, DealershipEntity incentiveCampaign)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int campaignId, int dealershipId)
        {
            throw new NotImplementedException();
        }

        public DealershipEntity ReadById(int dealershipId)
        {
            throw new NotImplementedException();
        }

        public List<DealershipEntity> ReadByCampaign(int campaign)
        {
            throw new NotImplementedException();
        }

        public List<DealershipEntity> ReadByDealer(int dealerId)
        {
            throw new NotImplementedException();
        }        
    }
}
