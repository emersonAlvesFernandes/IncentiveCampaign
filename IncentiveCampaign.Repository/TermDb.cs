using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public interface ITermDb
    {
        bool Upload(Term term);

        byte[] Download(int TermId);
    }

    public class TermDb : ITermDb
    {
        public bool Upload(Term term)
        {
            throw new NotImplementedException();
        }

        public byte[] Download(int TermId)
        {
            throw new NotImplementedException();
        }
    }
}
