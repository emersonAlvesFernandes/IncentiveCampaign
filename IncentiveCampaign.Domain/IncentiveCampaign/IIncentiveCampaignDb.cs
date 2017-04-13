using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.IncentiveCampaign
{
    public interface IIncentiveCampaignDb
    {
        IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign);

        IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign);

        bool Delete(int campaignId);

        List<IncentiveCampaignEntity> ReadAll();

        IncentiveCampaignEntity ReadById(int id);

        List<IncentiveCampaignEntity> ReadByDealer(int dealerId);

    }
}
