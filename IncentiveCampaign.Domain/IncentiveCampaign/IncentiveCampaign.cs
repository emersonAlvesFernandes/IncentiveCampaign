using IncentiveCampaign.Domain.IncentiveCampaign.ViewModels;
using System;
using System.Collections.Generic;

namespace IncentiveCampaign.Domain.IncentiveCampaign
{
    public class IncentiveCampaignEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public bool AgreementLetterNeeded { get; set; }

        public List<Dealership.Dealership> Dealerships { get; set; }

        public List<Term.Term> Terms { get; set; }


        public IncentiveCampaignEntity ToIncentiveCampaign(IncentiveCampaignCreate createObj)
        {
            var campaign = new IncentiveCampaignEntity()
            {
                Id = createObj.Id,
                Name = createObj.Name,
                StartDate = createObj.StartDate,
                EndDate = createObj.EndDate,
                //IsActive =createObj.IsActive,
                AgreementLetterNeeded = createObj.AgreementLetterNeeded,                                
            };
            
            return campaign;
        }
    }
}
