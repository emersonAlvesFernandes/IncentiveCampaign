using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Dealership.ViewModels
{
    public class DealershipSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public bool AgreementLetterSent { get; set; }
    }
}
