using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.Dealership.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{

    public interface IDealershipDb
    {
        Dealership Create(Dealership incentiveCampaign, int campaignId);

        Dealership ReadById(int dealershipId);

        List<Dealership> ReadByCampaign(int campaign);

        List<Dealership> ReadByDealer(int dealerId);
    }

    public class DealershipDb : IDealershipDb
    {
        public Dealership Create(Dealership incentiveCampaign, int campaignId)
        {
            throw new NotImplementedException();
        }

        public Dealership ReadById(int dealershipId)
        {
            throw new NotImplementedException();
        }

        public List<Dealership> ReadByCampaign(int campaign)
        {
            throw new NotImplementedException();
        }

        public List<Dealership> ReadByDealer(int dealerId)
        {
            throw new NotImplementedException();
        }        
    }
}
