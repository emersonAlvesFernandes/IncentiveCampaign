using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.Api.Models.Dealer
{
    public class DealerIds
    {
        public List<int> Ids { get; set; }

        public int CampaignId { get; set; }
    }
}