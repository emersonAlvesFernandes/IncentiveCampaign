using IncentiveCampaign.Domain.Dealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.Api.Models.Dealership
{
    public class DealershipSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public bool AgreementLetterSent { get; set; }

        public DealershipEntity ToDealershipEntity(DealershipSummary d)
        {
            var ret = new DealershipEntity
            {
                Id = d.Id,
                Name = d.Name,
                Cnpj = d.Cnpj,
                AgreementLetterSent = d.AgreementLetterSent,
            };

            return ret;
        }

        public List<DealershipEntity> ToDealershipEntity(List<DealershipSummary> list)
        {
            var collection = new List<DealershipEntity>();

            foreach (var l in list)
            {
                var ret = new DealershipEntity
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