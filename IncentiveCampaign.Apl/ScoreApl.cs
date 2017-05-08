using IncentiveCampaign.Domain.Score;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IncentiveCampaign.Apl
{
    public interface IScoreApl
    {
        //eliminar este método que será obtido pelo Statement
        List<ScoreEntity> GetByDealer(int dealerId, DateTime from, DateTime To);

        //dados obtidos deste método serão referentes ao history
        List<ScoreEntity> GetByDealer(int dealerId);

        List<ScoreEntity> GetOnlyValid(int dealerId);

        ScoreEntity CreateScore(int? campaignId, int dealershipId, int dealerId, ScoreEntity score);

        IDictionary<int, string> WriteDown(List<int> scoreIds);

        List<ScoreEntity> GetByDealerAndDealership(int dealershipId, int dealerId);
    }

    public class ScoreApl : IScoreApl
    {
        private readonly IScoreDb scoreDb;
        private readonly IDealershipApl dealershipApl;
        private readonly IIncentiveCampaignApl campaignApl;
        private readonly IDealerApl dealerApl;


        public ScoreApl()
        {
            this.scoreDb = new ScoreDb();
            this.dealershipApl = new DealershipApl();
            this.campaignApl = new IncentiveCampaignApl();
        }

        public ScoreApl(IScoreDb scoreDb, IDealershipApl dealershipApl,IIncentiveCampaignApl campaignApl, IDealerApl dealerApl)
        {
            this.scoreDb = scoreDb;
            this.dealershipApl = dealershipApl;
            this.campaignApl = campaignApl;
            this.dealerApl = dealerApl;
        }

        public List<ScoreEntity> GetByDealer(int dealerId, DateTime from, DateTime To)
        {
            var collection = scoreDb.ReadByDealer(dealerId, from, To);

            return collection;
        }

        public List<ScoreEntity> GetByDealer(int dealerId)
        {
            var collection = scoreDb.ReadByDealer(dealerId);

            return collection;
        }

        public ScoreEntity CreateScore(int? campaignId, int dealershipId, int dealerId, ScoreEntity score)
        {            
            if(campaignId == 0)
            {
                campaignApl.Check((int)campaignId);
            }

            dealerApl.Check(dealerId);

            dealershipApl.Check(dealershipId);
            
            return scoreDb.CreateScore(campaignId, dealershipId, score);            
        }

        public List<ScoreEntity> GetOnlyValid(int dealerId)
        {
            dealerApl.Check(dealerId);

            var allScores = scoreDb.ReadByDealer(dealerId, null, null);

            var collection = allScores
                .Where(x => x.IsBlocked = false)
                .ToList();

            return collection;
        }

        public IDictionary<int, string> WriteDown(List<int> scoresIds)
        {
            //TODO: Resolver erro: estou criando ponto total abaixo, mas não está agrupado por campanha

            var foundScores = this.GetFoundScores(scoresIds);

            var notfoundScores = this.GetNotFoundScores(scoresIds);
            
            var total = foundScores
                .Select(x => x.Value)
                .Sum();

            var description = "baixa de ponto(s) con id(s) "
                + string.Join(", ", foundScores.Select(x=>x.Id.ToString()));

            var scoreDown = new WriteDownScore(
                total,                 
                description);
            
            using (var transaction = new TransactionScope())
            {
                scoreDb.WriteDown(scoreDown);

                this.Invalidate(foundScores);

                transaction.Complete();
            }
            
            var result = this.GetWriteDownScoresDictionary(foundScores, notfoundScores);
            
            return result;
        }

        private List<ScoreEntity> GetFoundScores(List<int> scoresIds)
        {
            var foundScores = new List<ScoreEntity>();

            foreach (var s in scoresIds)
            {
                var score = this.GetById(s);

                if (score == null)
                    continue;

                foundScores.Add(score);                
            }

            return foundScores;
        }

        private List<ScoreEntity> GetNotFoundScores(List<int> scoresIds)
        {
            var notFoundScores = new List<ScoreEntity>();

            foreach (var s in scoresIds)
            {
                var score = this.GetById(s);

                if (score != null)
                    continue;

                notFoundScores.Add(score);
            }

            return notFoundScores;
        }

        private ScoreEntity GetById(int scoreId)
        {
            try
            {
                return scoreDb.ReadById(scoreId);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private void Invalidate(List<ScoreEntity> scores)
        {
            var ids = scores.Select(x => x.Id);

            foreach(var id in ids)
                scoreDb.Invalidate(id);            
        }

        private Dictionary<int, string> GetWriteDownScoresDictionary(
            List<ScoreEntity> foundScores,
            List<ScoreEntity> notFoundScores)
        {
            var allScoresResult = new Dictionary<int, string>();

            foreach (var f in foundScores)
            {
                scoreDb.Invalidate(f.Id);
                allScoresResult.Add(f.Id, "ok");
            }

            foreach (var notFound in notFoundScores)
            {
                allScoresResult.Add(notFound.Id, "score.not.found");
            }

            return allScoresResult;
        }

        public List<ScoreEntity> GetByDealerAndDealership(int dealershipId, int dealerId)
        {
            dealerApl.Check(dealerId);

            return scoreDb.ReadByDealerAndDealership(dealershipId, dealerId);
        }                        
    }
}
