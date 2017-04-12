﻿using IncentiveCampaign.Domain.Dealership;
using System;
using System.Collections.Generic;

namespace IncentiveCampaign.Domain.IncentiveCampaign
{
    public class IncentiveCampaignEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public bool AgreementLetterNeeded { get; set; }

        public List<Dealership.DealershipEntity> Dealerships { get; set; }
       
    }
}
