using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Dealer
{
    public interface IDealerDb
    {
        List<DealerEntity> ReadAll();

        DealerEntity ReadOnCorporateBase(int userId);

        List<DealerEntity> ReadByDealerShip(int dealership);

        DealerEntity ReadByIdAndCampaignId(int dealerId, int campaignId);

        bool RegisterToCampaign(int campaignId, int dealerId);
    }
}
