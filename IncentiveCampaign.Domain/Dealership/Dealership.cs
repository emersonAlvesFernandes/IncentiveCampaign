using IncentiveCampaign.Domain.Dealership.ViewModels;
using System.Collections.Generic;

namespace IncentiveCampaign.Domain.Dealership
{
    public class Dealership
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public bool AgreementLetterSent { get; set; }

        public List<Dealer.Dealer> Dealers { get; set; }

        public Dealership ToDealership (DealershipSummary d)
        {
            var ret = new Dealership
            {
                Id = d.Id,
                Name = d.Name,
                Cnpj = d.Cnpj,
                AgreementLetterSent = d.AgreementLetterSent,                
            };

            return ret;
        }

        public List<Dealership> ToDealership(List<DealershipSummary> list)
        {
            var collection = new List<Dealership>();

            foreach (var l in list)
            {
                var ret = new Dealership
                {
                    Id = l.Id,
                    Name = l.Name,
                    Cnpj = l.Cnpj,
                    AgreementLetterSent = l.AgreementLetterSent,
                };
            }

            return collection;
        }

    }
}
