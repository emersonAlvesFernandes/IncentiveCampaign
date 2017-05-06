using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Score
{
    public interface IScoreDb
    {
        ScoreEntity ReadById(int scoreId);

        List<ScoreEntity> ReadByDealer(int dealer);

        List<ScoreEntity> ReadByDealer(int dealer, DateTime? From, DateTime? To);

        List<ScoreEntity> ReadByDealerAndDealership(int dealershipId, int dealerId);

        ScoreEntity CreateScore(ScoreEntity score);

        bool WriteDown(WriteDownScore score);

        void Invalidate(int ids);
    }
}
