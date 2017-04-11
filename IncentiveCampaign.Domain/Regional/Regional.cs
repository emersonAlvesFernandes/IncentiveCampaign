using System.Collections.Generic;

namespace IncentiveCampaign.Domain.Regional
{
    class Regional
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Dealership.Dealership> Dealerships { get; set; }
    }
}
