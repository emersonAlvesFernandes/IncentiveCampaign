using IncentiveCampaign.Domain.Dealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.WebApi.Models.Dealership
{
    public class DealershipCreate
    {
        public int CampaignId { get; set; }

        //public int Id { get; set; }

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public bool AgreementLetterSent { get; set; }

        public DealershipEntity ToDealershipEntity(DealershipCreate vm)
        {
            var entity = new DealershipEntity
            {
                //Id = vm.Id,
                Name = vm.Name,
                Cnpj = vm.Cnpj,
                AgreementLetterSent = vm.AgreementLetterSent
            };

            return entity;
        }

        public List<DealershipEntity> ToDealershipEntity(List<DealershipCreate> list)
        {
            var collection = new List<DealershipEntity>();

            foreach (var l in list)
            {
                var ret = new DealershipEntity
                {
                    //Id = l.Id,
                    Name = l.Name,
                    Cnpj = l.Cnpj,
                    AgreementLetterSent = l.AgreementLetterSent,
                };
            }

            return collection;
        }
    }
}