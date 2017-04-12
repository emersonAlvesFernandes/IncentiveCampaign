using IncentiveCampaign.Domain.Dealership;
using System.Collections.Generic;

namespace IncentiveCampaign.Domain.Regional
{
    class RegionalEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Dealership.DealershipEntity> Dealerships { get; set; }
    }
}
