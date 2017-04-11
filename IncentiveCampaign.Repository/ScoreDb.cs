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
        Score ReadById(int scoreId);

        List<Score> ReadByDealer(int dealer);

        List<Score> ReadByDealer(int dealer, DateTime? From, DateTime? To);

        List<Score> ReadByDealerAndDealership(int dealershipId, int dealerId);

        Score CreateScore(Score score);

        bool WriteDown(Score score);

        void Invalidate(int ids);
    }

    public class ScoreDb : IScoreDb
    {
        public Score ReadById(int scoreId)
        {
            throw new NotImplementedException();
        }

        public List<Score> ReadByDealer(int dealer)
        {
            throw new NotImplementedException();
        }

        public List<Score> ReadByDealer(int dealer, DateTime? From, DateTime? To)
        {
            //Nesta proc cdevo obter todos os pontos, 
            //fazer em subselect para identificar dos pontos, quais são bloqueados
            throw new NotImplementedException();
        }

        public List<Score> ReadByDealerAndDealership(int dealershipId, int dealerId)
        {
            throw new NotImplementedException();
        }

        public Score CreateScore(Score score)
        {
            throw new NotImplementedException();
        }

        public bool WriteDown(Score score)
        {
            throw new NotImplementedException();
        }

        public void Invalidate(int ids)
        {
            throw new NotImplementedException();
        }
    }
}
