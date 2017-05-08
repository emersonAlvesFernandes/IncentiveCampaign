using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Dealer
{
    public class AgreementLetter
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string Extention { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
