using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.Api.Models.IncentiveCampaign
{
    public class IncentiveCampaignSummary
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public bool AgreementLetterNeeded { get; set; }

        public IncentiveCampaignSummary ToSummary(IncentiveCampaignCreate c)
        {
            var ret = new IncentiveCampaignSummary
            {
                Id = c.Id,
                Name = c.Name,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                AgreementLetterNeeded = c.AgreementLetterNeeded,
            };

            return ret;
        }
    }

}