using IncentiveCampaign.Domain.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Statement
{
    public class StatementItem
    {
        public int DealerId { get; set; }

        public string DealerName { get; set; } //*

        public int ScoreValue { get; set; }

        public int IdCampaign { get; set; }

        public bool CampaignRequiresAgreementLetter { get; set; }

        public int DealershipId { get; set; }

        public string DealershipName { get; set; }//*

        public bool DealershipSentAgreementLetter { get; set; }

        public bool UserAcceptedTerm { get; set; }

        public string RegionalName { get; set; }//*

        public bool IsValid()
        {
            //specific for dealers not related to campaigns
            if (IdCampaign == 0)
                return true;

            if (this.UserAcceptedTerm == false)
                return false;

            if (this.CampaignRequiresAgreementLetter == true
                && DealershipSentAgreementLetter == false)
                return false;

            return true;
        }
    }

    public class StatementItemRow
    {
        public string DealerName { get; set; }

        public int ValidScoreAmmount { get; set; }

        public int BlockedScoreAmmount { get; set; }

        public List<string> DealershipNames { get; set; }

        public List<string> RegionalNames { get; set; }


        public List<StatementItemRow> ToStatementRows(List<StatementItem> collection)
        {
            var satementItemRows = new List<StatementItemRow>();

            var users = collection
                .Select(x => x.DealerId)
                .Distinct()
                .ToList();

            foreach (var u in users)
            {
                //get list of user scores
                var scores = collection
                    .Where(x => x.DealerId == u)
                    .ToList();

                var validScores = 0;
                var blockedScores = 0;
                foreach (var s in scores)
                {
                    // get valid scores
                    if (s.IsValid())
                        validScores += s.ScoreValue;

                    // get invalid scores
                    else
                        blockedScores += s.ScoreValue;
                }

                // get list of dealerships
                var dealershipNames = scores
                    .Select(x => x.DealershipName)
                    .Distinct()
                    .ToList();

                //get list of regionals
                var regionals = new List<string>();
                var regional = scores
                    .Select(x => x.RegionalName)
                    .Distinct()
                    .ToList();

                // get name
                var name = scores
                    .Select(x => x.DealerName)
                    .FirstOrDefault();

                var row = new StatementItemRow
                {
                    DealerName = name,
                    ValidScoreAmmount = validScores,
                    BlockedScoreAmmount = blockedScores,
                    DealershipNames = dealershipNames,
                    RegionalNames = regionals
                };

                satementItemRows.Add(row);
            }

            return satementItemRows;
        }

    }
}
