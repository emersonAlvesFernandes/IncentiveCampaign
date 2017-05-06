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
        List<StatementItemRow> GetStatement();
    }

    public class StatementApl : IStatementApl
    {
        private readonly IStatementDb statementDb;

        public StatementApl()
        {
            this.statementDb = new StatementCorporateDb();
        }

        public List<StatementItemRow> GetStatement()
        {
            var statementItens = statementDb.ReadStatement();

            var statementItemRow = new StatementItemRow();

            var returnCollection = statementItemRow.ToStatementRows(statementItens);

            return returnCollection;
        }
    }
}
