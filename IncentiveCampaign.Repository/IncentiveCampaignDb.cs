using IncentiveCampaign.Domain.IncentiveCampaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public interface IIncentiveCampaignDb
    {
        IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign);

        List<IncentiveCampaignEntity> ReadAll();

        List<IncentiveCampaignEntity> ReadByDealer(int dealerId);

        IncentiveCampaignEntity ReadById(int Id);
    }

    public class IncentiveCampaignDb : IIncentiveCampaignDb
    {
        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            throw new NotImplementedException();
        }

        public List<IncentiveCampaignEntity> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<IncentiveCampaignEntity> ReadByDealer(int dealerId)
        {
            throw new NotImplementedException();
        }

        public List<IncentiveCampaignEntity> GetByDealership(int dealerId)
        {
            throw new NotImplementedException();
        }

        public IncentiveCampaignEntity ReadById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
