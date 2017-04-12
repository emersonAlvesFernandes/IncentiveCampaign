using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.WebApi.Models.Score
{
    public class PeriodScore
    {
        public int DealerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}