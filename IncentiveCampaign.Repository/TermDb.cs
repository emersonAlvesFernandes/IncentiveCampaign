using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public interface ITermDb
    {
        int Upload(TermEntity term, int campaignId);

        TermEntity Download(int TermId);

        List<TermEntity> ReadByCampaign(int campaignId);
    }

    public class TermDb : ITermDb
    {
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
