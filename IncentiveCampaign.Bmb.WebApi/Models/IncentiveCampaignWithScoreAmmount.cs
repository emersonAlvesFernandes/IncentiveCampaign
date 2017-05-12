using IncentiveCampaign.Domain.IncentiveCampaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncentiveCampaign.Bmb.WebApi.Models
{
    public class IncentiveCampaignWithScoreAmmount
    {
        public string Name { get; set; }

        //public DateTime StartDate { get; set; }

        //public DateTime EndDate { get; set; }

        public string Validity { get; set; }

        public bool Status { get; set; }

        public int Value { get; set; }

        public List<IncentiveCampaignWithScoreAmmount> ToIncentiveCampaignWithScoreAmmount(List<IncentiveCampaignEntity> entityCollection)
        {
            var collection = new List<IncentiveCampaignWithScoreAmmount>();

            foreach (var campaign in entityCollection)
            {
                var row = new IncentiveCampaignWithScoreAmmount();
                row.Name = campaign.Name;
                row.Validity = campaign.StartDate.ToString() + " a " + campaign.EndDate.ToString();

                var dealer = campaign
                    .Dealerships.FirstOrDefault()
                    .Dealers.FirstOrDefault();

                row.Status = dealer.AcceptedTerm;

                var total = dealer
                    .Scores.Select(x => x.Value).Sum();

                collection.Add(row);

            }

            return collection;
        }

    }
}