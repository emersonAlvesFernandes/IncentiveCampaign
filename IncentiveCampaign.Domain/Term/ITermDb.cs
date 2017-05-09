using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Term
{
    public interface ITermDb
    {
        bool Register(int incentiveCampaignId, TermEntity term, string codUser);

        bool Upload(int campaignId, TermEntity term);

        TermEntity Download(int TermId);

        List<TermEntity> ReadByCampaign(int campaignId);

        bool Delete(int termId);
    }
}
