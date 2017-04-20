using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{
    public interface IStatementApl
    {
        List<StatementItem> GetStatement();
    }

    public class StatementApl : IStatementApl
    {
        private readonly IStatementDb statementDb;

        public StatementApl()
        {
            this.statementDb = new StatementCorporateDb();
        }

        public List<StatementItem> GetStatement()
        {
            var collection = statementDb.ReadStatement();

            

            return collection;
        }
    }
}
