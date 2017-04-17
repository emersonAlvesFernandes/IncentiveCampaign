using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.WebApi.Models.Term
{
    public class TermCreate
    {        
        public string Name { get; set; }

        public byte[] Contents { get; set; }

        public string Extention { get; set; }

        //public long TotalBytes { get; set; }
    }
    public class TermCreateValidator : AbstractValidator<TermCreate>
    {
        public TermCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Term Name cannot be empty.")
                                        .Length(0, 25).WithMessage("Term Name cannot be more than 25 characters.");

            RuleFor(x => x.Extention).NotEmpty().WithMessage("Term Extention cannot be empty.")
                                        .Length(0, 3).WithMessage("Term Extention cannot be more than 3 characters.");

            //RuleFor(x => x.StartDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            //RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}