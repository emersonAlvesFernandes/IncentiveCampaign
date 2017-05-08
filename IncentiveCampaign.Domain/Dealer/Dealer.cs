using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Dealer
{
    public class DealerEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public bool AcceptedTerm { get; set; }

        public bool IsValidated { get; set; }

        public List<DealershipEntity> Dealerships { get; set; }

        public List<ScoreEntity> Scores { get; set; }

        public bool IsValid { get; set; }

        public bool IsValidatedDealer()
        {
            if (this.IsValid == false)
                return false;
            return true;
        }

        public bool IsRegistered()
        {
            if (this.Id == 0)
                return false;

            return true;
        }

    }
}

