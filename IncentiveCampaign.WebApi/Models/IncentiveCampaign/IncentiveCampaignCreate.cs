using FluentValidation;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.WebApi.Models.Dealership;
using System;
using System.Collections.Generic;

namespace IncentiveCampaign.WebApi.Models
{
    public class IncentiveCampaignCreate
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool AgreementLetterNeeded { get; set; }

        public List<DealershipCreate> Dealerships { get; set; }

        public IncentiveCampaignEntity ToIncentiveCampaignEntity(IncentiveCampaignCreate createObj)
        {
            var campaign = new IncentiveCampaignEntity()
            {
                //Id = createObj.Id,
                Name = createObj.Name,
                StartDate = createObj.StartDate,
                EndDate = createObj.EndDate,
                //IsActive =createObj.IsActive,
                AgreementLetterNeeded = createObj.AgreementLetterNeeded,
            };

            return campaign;
        }
    }

    public class IncentiveCampaignCreateValidator : AbstractValidator<IncentiveCampaignCreate>
    {
        public IncentiveCampaignCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Incentive Campaign Name cannot be empty.")
                                        .Length(0, 100).WithMessage("Incentive Campaign Name cannot be more than 100 characters.");


            //RuleFor(x => x.StartDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
