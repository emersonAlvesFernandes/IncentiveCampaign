using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.IncentiveCampaign.ViewModels
{
    public class IncentiveCampaignCreate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public bool AgreementLetterNeeded { get; set; }

        public List<Dealership.ViewModels.DealershipSummary> Dealerships { get; set; }

        public List<Term.Term> Terms { get; set; }        
    }
}
