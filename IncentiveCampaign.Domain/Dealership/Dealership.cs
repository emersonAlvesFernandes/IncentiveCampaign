using IncentiveCampaign.Domain.Dealer;
using System;
using System.Collections.Generic;

namespace IncentiveCampaign.Domain.Dealership
{
    public class DealershipEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public bool AgreementLetterSent { get; set; }

        public List<DealerEntity> Dealers { get; set; }      

        public bool IsRegistered()
        {
            if (this.Id == 0)
                return false;
            return true;
        }

        public bool IsValidByAgreementLetter(bool agreementLetterIsNeeded)
        {
            if (agreementLetterIsNeeded == true &&
                this.AgreementLetterSent == false)
                return false;
            return true;
        }

    }
}
