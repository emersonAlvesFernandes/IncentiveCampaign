using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Score.RequestModel
{
    public class ScoresIds
    {
        public List<int> Ids { get; set; }

        public int CampaignId { get; set; }
    }
}
