using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Score
{
    public class Score
    {
        public Score()
        {

        }

        public Score(int id, int value, bool isblocked, string description, string proposal, string contract, string policy)
        {
            this.Id = id;
            this.Value = value;
            this.IsBlocked = isblocked;
            this.Descriprion = description;
            this.Proposal = proposal;
            this.Contract = contract;
            this.Policy = policy;                
        }

        public int Id { get; set; }

        public int Value { get; set; }

        public bool IsBlocked { get; set; }

        public string Descriprion { get; set; }

        public string Proposal { get; set; }

        public string Contract { get; set; }

        public string Policy { get; set; }
    }
}
