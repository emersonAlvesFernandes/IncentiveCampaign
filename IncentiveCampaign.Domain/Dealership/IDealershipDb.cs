using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Dealership
{
    public interface IDealershipDb
    {
        bool Register(int campaignId, DealershipEntity dealership);

        bool Delete(int campaignId, int dealershipId);

        //DealershipEntity ReadById(int dealershipId);

        List<DealershipEntity> ReadByCampaign(int campaign);

        List<DealershipEntity> ReadByDealer(int dealerId);
    }
}
