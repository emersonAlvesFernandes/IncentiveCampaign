using FluentValidation;
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

        //public string Name { get; set; }

        //public string Cnpj { get; set; }

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

    public class DealershipCreateValidator : AbstractValidator<DealershipCreate>
    {
        public DealershipCreateValidator()
        {
            //RuleFor(x => x.xxx).NotEmpty().WithMessage("xxxxx Name cannot be empty.")
            //                            .Length(0, 100).WithMessage("Incentive Campaign Name cannot be more than 100 characters.");

            //RuleFor(x => x.StartDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}