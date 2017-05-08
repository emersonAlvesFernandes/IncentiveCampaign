using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.WebApi.Models.Dealer
{
    public class AgreementLetterCreate
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string Extention { get; set; }        
    }
}