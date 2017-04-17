using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Term
{
    public interface ITermDb
    {
        TermEntity Register(int incentiveCampaignId, TermEntity term);

        int Upload(TermEntity term, int campaignId);

        TermEntity Download(int TermId);

        List<TermEntity> ReadByCampaign(int campaignId);
    }
}
