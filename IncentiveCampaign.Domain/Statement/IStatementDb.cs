using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Statement
{
    public interface IStatementDb
    {
        List<StatementItem> ReadStatement();
    }
}
