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
        public int IdUser { get; set; }

        public int ScoreValue { get; set; }

        public int IdCampaign { get; set; }

        public bool CampaignRequiresAgreementLetter { get; set; }

        public string DealershipId { get; set; }

        public bool DealershipSentAgreementLetter { get; set; }

        public bool UserAcceptedTerm { get; set; }

        public bool IsValid()
        {
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
        public int IdUser { get; set; }

        public int ValidScoreAmmount { get; set; }

        public int BlockedScoreAmmount { get; set; }
        
        public List<string> DealershipNames { get; set; }

        public List<string> RegionalNames { get; set; }

        public List<StatementItemRow> ToStatementRow(List<StatementItem> collection)
        {
            var satementItemRows = new List<StatementItemRow>();

            var users = collection
                .Select(x => x.IdUser)
                .Distinct()
                .ToList();

            foreach(var u in users)
            {
                var scores = collection
                    .Where(x => x.IdUser == u)
                    .ToList();

                var validScores = 0;
                var blockedScores = 0;

                foreach (var s in scores)
                {
                    if (s.IsValid())
                        validScores += s.ScoreValue;
                    else
                        blockedScores += s.ScoreValue;
                }

                var dealershipNames = scores
                    .Select(x => x.DealershipId)
                    .Distinct()
                    .ToList();

                var regionals = new List<string>();
                //TODO
                //var regional = scores
                //    .Select(x => x.Regional)
                //    .Distinct()
                //    .ToList();

                var row = new StatementItemRow
                {
                    IdUser = u,
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
