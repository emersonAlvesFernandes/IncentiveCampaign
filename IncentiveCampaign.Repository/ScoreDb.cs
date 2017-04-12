using IncentiveCampaign.Domain.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public interface IScoreDb
    {
        ScoreEntity ReadById(int scoreId);

        List<ScoreEntity> ReadByDealer(int dealer);

        List<ScoreEntity> ReadByDealer(int dealer, DateTime? From, DateTime? To);

        List<ScoreEntity> ReadByDealerAndDealership(int dealershipId, int dealerId);

        ScoreEntity CreateScore(ScoreEntity score);

        bool WriteDown(ScoreEntity score);

        void Invalidate(int ids);
    }

    public class ScoreDb : IScoreDb
    {
        public ScoreEntity ReadById(int scoreId)
        {
            throw new NotImplementedException();
        }

        public List<ScoreEntity> ReadByDealer(int dealer)
        {
            throw new NotImplementedException();
        }

        public List<ScoreEntity> ReadByDealer(int dealer, DateTime? From, DateTime? To)
        {
            //Nesta proc cdevo obter todos os pontos, 
            //fazer em subselect para identificar dos pontos, quais são bloqueados
            throw new NotImplementedException();
        }

        public List<ScoreEntity> ReadByDealerAndDealership(int dealershipId, int dealerId)
        {
            throw new NotImplementedException();
        }

        public ScoreEntity CreateScore(ScoreEntity score)
        {
            throw new NotImplementedException();
        }

        public bool WriteDown(ScoreEntity score)
        {
            throw new NotImplementedException();
        }

        public void Invalidate(int ids)
        {
            throw new NotImplementedException();
        }
    }
}
