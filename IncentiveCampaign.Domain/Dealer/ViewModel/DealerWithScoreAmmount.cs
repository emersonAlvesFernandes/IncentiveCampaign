using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Domain.Dealer.ViewModel
{
    public class DealerWithScoreAmmount
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public int ScoreAmmount { get; set; }

        public List<DealerWithScoreAmmount> ToDealerWithScoreAmmount(List<Dealer> dealers)
        {
            var collection = new List<DealerWithScoreAmmount>();

            foreach (var d in dealers)
            {
                var total = d.Scores.Select(x => x.Value).Sum();

                var row = new DealerWithScoreAmmount
                {
                    Id = d.Id,
                    Name = d.Name,
                    Cpf = d.Cpf,
                    ScoreAmmount = total

                };

                collection.Add(row);
            }

            return collection;
        }
    }
}
