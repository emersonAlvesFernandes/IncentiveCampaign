using System.Collections.Generic;

namespace IncentiveCampaign.Domain.Dealer
{
    public class Dealer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public bool AcceptedTerm { get; set; }

        public List<Dealership.Dealership> Dealerships { get; set; }

        public List<Score.Score> Scores { get; set; }
    }
}
