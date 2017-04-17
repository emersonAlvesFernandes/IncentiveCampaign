using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public class TermDb : ITermDb
    {
        public TermEntity Register(int incentiveCampaignId, TermEntity term)
        {
            throw new NotImplementedException();
        }

        public int Upload(TermEntity term, int campaignId)
        {
            throw new NotImplementedException();
        }

        public TermEntity Download(int TermId)
        {
            throw new NotImplementedException();
        }

        public List<TermEntity> ReadByCampaign(int campaignId)
        {
            throw new NotImplementedException();
        }
    }
}
