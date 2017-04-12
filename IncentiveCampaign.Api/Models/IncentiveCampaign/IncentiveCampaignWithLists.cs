using IncentiveCampaign.Api.Models.Dealership;
using IncentiveCampaign.Api.Models.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.Api.Models.IncentiveCampaign
{
    public class IncentiveCampaignWithLists
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public bool AgreementLetterNeeded { get; set; }

        public List<DealershipSummary> Dealerships { get; set; }

        public List<TermSummary> Terms { get; set; }
    }
}