using IncentiveCampaign.WebApi.Models.Dealership;
using IncentiveCampaign.WebApi.Models.Term;
using System;
using System.Collections.Generic;

namespace IncentiveCampaign.WebApi.Models.IncentiveCampaign
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