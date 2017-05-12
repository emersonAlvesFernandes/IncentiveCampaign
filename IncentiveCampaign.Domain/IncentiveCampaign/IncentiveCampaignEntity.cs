using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.Term;
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

        public DateTime CreationDate { get; set; }

        public string UserName { get; set; }

        public bool AgreementLetterNeeded { get; set; }        
        
        public List<DealershipEntity> Dealerships { get; set; }

        public List<TermEntity> Terms { get; set; }                

        public void Initialize()
        {
            this.CreationDate = DateTime.Now;
            this.IsActive = true;
            
            if (this.StartDate > this.EndDate)
                throw new Exception("Invalid.Dates");
        }
    }
}
