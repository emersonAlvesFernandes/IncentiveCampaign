using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Term
{
    public class TermEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Contents { get; set; }

        public string Extention { get; set; }
    }
}
