using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Score
{
    public class WriteDownScore
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public bool IsBlocked { get; set; }

        public string Descriprion { get; set; }
        
        public WriteDownScore()
        {

        }

        public WriteDownScore(int value, string description)
        {
            this.Id = 0;
            this.Value = value;
            this.IsBlocked = false;
            this.Descriprion = description;            
        }
    }
}
