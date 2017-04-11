using IncentiveCampaign.Domain.Score;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{
    public interface IScoreApl
    {
        List<Score> GetByDealer(int dealerId, DateTime from, DateTime To);

        List<Score> GetOnlyValid(int dealerId);

        Score CreateScore(Score score);

        IDictionary<int, string> WriteDown(List<int> scoreIds);
    }

    public class ScoreApl : IScoreApl
    {
        private readonly IScoreDb scoreDb;

        public ScoreApl()
        {
            this.scoreDb = new ScoreDb();
        }

        public ScoreApl(IScoreDb scoreDb)
        {
            this.scoreDb = scoreDb;
        }

        public List<Score> GetByDealer(int dealerId, DateTime from, DateTime To)
        {
            var collection = scoreDb.ReadByDealer(dealerId, from, To);

            return collection;
        }

        public Score CreateScore(Score score)
        {
            return scoreDb.CreateScore(score);

            //TODO: Inserir o termo

        }

        public List<Score> GetOnlyValid(int dealerId)
        {
            var allScores = scoreDb.ReadByDealer(dealerId, null, null);

            var collection = allScores
                .Where(x => x.IsBlocked = false)
                .ToList();

            return collection;
        }

        public IDictionary<int, string> WriteDown(List<int> scoresIds)
        {
            //TODO: Resolver erro: estou criando ponto total abaixo, mas não está agrupado por campanha

            var notFoundscores = new List<Score>(); //new Dictionary<int, string>();
            var foundScores = new List<Score>();

            foreach(var s in scoresIds)
            {
                var score = this.ReadById(s);

                if (score == null)
                    //notFoundscores.Add(s, "score.not.found");
                    notFoundscores.Add(score);
                else
                    foundScores.Add(score);
            }            

            var total = foundScores
                .Select(x => x.Value)
                .Sum();

            var description = "baixa de ponto(s) con id(s) "
                + string.Join(", ", foundScores.Select(x=>x.Id.ToString()));

            var scoreDown = new Score(0, 
                total, 
                false,
                description,
                string.Empty, 
                string.Empty, 
                string.Empty);

            //TODO: Colocar em transaction
            scoreDb.WriteDown(scoreDown);


            var allScoresResult = new Dictionary<int, string>();

            foreach (var f in foundScores)
            {
                scoreDb.Invalidate(f.Id);
                allScoresResult.Add(f.Id, "ok");
            }

            foreach(var notFound in notFoundscores)
            {
                allScoresResult.Add(notFound.Id, "score.not.found");
            }

            return allScoresResult;
        }

        private Score ReadById(int scoreId)
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

        private void Invalidate(List<int> ids)
        {
            foreach(var id in ids)
                scoreDb.Invalidate(id);
            
        }
    }
}
