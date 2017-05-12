using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IncentiveCampaign.AppService
{
    public interface IIncentiveCampaignAppService
    {
        IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign);

        IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign);

        bool Delete(int campaignId);

        List<IncentiveCampaignEntity> GetAll();

        IncentiveCampaignEntity GetById(int campaignId);

        List<IncentiveCampaignEntity> GetByDealer(int dealerId);

        List<IncentiveCampaignEntity> GetByDealership(int dealershipId);

        List<IncentiveCampaignEntity> GetManagerCampaigns(int dealershipId, int managerId);

        void Check(int campaignId);
    }

    public class IncentiveCampaignAppService : IIncentiveCampaignAppService
    {
        private readonly IIncentiveCampaignApl incentiveCampaignApl;
        private readonly IDealershipApl dealershipApl;
        private readonly IDealerApl dealerApl;
        private readonly IScoreApl scoreApl;
        private readonly ITermApl termApl;

        public IncentiveCampaignAppService(IIncentiveCampaignApl incentiveCampaignApl,
            IDealershipApl dealershipApl,
            IDealerApl dealerApl,
            IScoreApl scoreApl,
            ITermApl termApl)
        {
            this.incentiveCampaignApl = incentiveCampaignApl;
            this.dealershipApl = dealershipApl;
            this.dealerApl = dealerApl;
            this.scoreApl = scoreApl;
            this.termApl = termApl;
        }

        public IncentiveCampaignAppService()
        {
            //this.incentiveCampaignDb = new IncentiveCampaignCorporateDb();            
            this.incentiveCampaignApl = new IncentiveCampaignApl();

            this.dealershipApl = new DealershipApl();
            this.dealerApl = new DealerApl();
            this.termApl = new TermApl();
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            using (var transaction = new TransactionScope())
            {
                incentiveCampaign.Initialize();

                incentiveCampaign = incentiveCampaignApl.Create(incentiveCampaign);
                
                foreach (var d in incentiveCampaign.Dealerships)
                {                    
                    var dealership = this.dealershipApl.Register(incentiveCampaign.Id, d);
                }

                foreach (var t in incentiveCampaign.Terms)
                {
                    var term = termApl.Register(incentiveCampaign.Id, t, incentiveCampaign.UserName);
                }

                transaction.Complete();
                return incentiveCampaign;
            }
        }

        public IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign)
        {            
            return this.incentiveCampaignApl.Update(incentiveCampaign);           
        }
        
        public bool Delete(int campaignId)
        {
            return this.incentiveCampaignApl.Delete(campaignId);
        }

        public List<IncentiveCampaignEntity> GetAll()
        {
            return incentiveCampaignApl.GetAll();
        }

        public List<IncentiveCampaignEntity> GetByDealer(int dealerId)
        {
            return incentiveCampaignApl.GetByDealer(dealerId);
        }

        public List<IncentiveCampaignEntity> GetByDealership(int dealershipId)
        {
            return incentiveCampaignApl.GetByDealer(dealershipId);
        }

        //Possivelmente apagar este método pois será utilizado o statement
        public List<IncentiveCampaignEntity> GetManagerCampaigns(int dealershipId, int managerId)
        {
            var campaigns = this.incentiveCampaignApl.GetByDealer(dealershipId); 

            foreach (var campaign in campaigns)
            {
                //var user = dealerDb.ReadByIdAndCampaignId(managerId, campaign.Id);
                var dealer = this.dealerApl.GetByIdAndCampaignId(managerId, campaign.Id);

                //var dealership = dealershipDb.ReadById(dealershipId);
                var dealership = this.dealershipApl.GetById(dealershipId);

                //user.Scores = scoreDb.ReadByDealerAndDealership(dealership.Id, user.Id);
                dealer.Scores = this.scoreApl.GetByDealerAndDealership(dealership.Id, dealer.Id);

                dealership.Dealers.Add(dealer);

                campaign.Dealerships.Add(dealership);
            }

            //N Campaigns >> 1 Dealerships >> 1 Dealer >> N Scores
            return campaigns;
        }

        public List<IncentiveCampaignEntity> GetEmployeeCampaigns(int dealershipId, int managerId)
        {
            var campaigns = this.incentiveCampaignApl.GetByDealer(dealershipId);

            foreach (var campaign in campaigns)
            {
                //var user = dealerDb.ReadByIdAndCampaignId(managerId, campaign.Id);
                var dealer = this.dealerApl.GetByIdAndCampaignId(managerId, campaign.Id);

                //var dealership = dealershipDb.ReadById(dealershipId);
                var dealership = this.dealershipApl.GetById(dealershipId);

                //user.Scores = scoreDb.ReadByDealerAndDealership(dealership.Id, user.Id);
                dealer.Scores = this.scoreApl.GetByDealerAndDealership(dealership.Id, dealer.Id);

                dealership.Dealers.Add(dealer);

                campaign.Dealerships.Add(dealership);
            }

            //N Campaigns >> 1 Dealerships >> 1 Dealer >> N Scores
            return campaigns;
        }

        public TermEntity Download(int termId)
        {
            //return termDb.Download(termId);
            return termApl.Download(termId);
        }

        public void Check(int id)
        {
            throw new NotImplementedException();
        }
    }
}
