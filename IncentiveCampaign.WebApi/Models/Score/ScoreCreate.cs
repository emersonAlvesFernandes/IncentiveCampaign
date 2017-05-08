using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.WebApi.Models.Score
{
    public class ScoreCreate
    {
        public int Value { get; set; }

        public bool IsBlocked { get; set; }

        public string Proposal { get; set; }

        public string Contract { get; set; }

        public string Policy { get; set; }

        public string Descriprion { get; set; }

        public int DealerId { get; set; }

        public int DealershipId { get; set; }

        public int? CampaignId { get; set; }
    }


    public class ScoreCreateValidator : AbstractValidator<ScoreCreate>
    {
        public ScoreCreateValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Value cannot be null.");
                
            RuleFor(x => x.Descriprion)
                .NotEmpty()
                .WithMessage("Description cannot be null.")
                .Length(0,30)
                .WithMessage("Descriptioncannot be more than 100 characters.");

            RuleFor(x => x.DealerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Dealer Id cannot be null.");

            RuleFor(x => x.DealershipId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Dealership Id cannot be null.");



        }
    }

}