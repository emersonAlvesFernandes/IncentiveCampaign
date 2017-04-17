using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Term;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{       
    
    public interface IIncentiveCampaignApl
    {
        IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign);

        IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign);

        bool Delete(int campaignId);

        List<IncentiveCampaignEntity> GetAll();

        IncentiveCampaignEntity GetById(int campaignId);

        List<IncentiveCampaignEntity> GetByDealer(int dealerId);

        List<IncentiveCampaignEntity> GetByDealership(int dealershipId);

        List<IncentiveCampaignEntity> GetManagerCampaigns(int dealershipId, int managerId);

        TermEntity UploadTerm(TermEntity term, int campaignId);

    }


    public class IncentiveCampaignApl : IIncentiveCampaignApl
    {
        private readonly IIncentiveCampaignDb incentiveCampaignDb;
        private readonly IDealershipDb dealershipDb;
        private readonly IDealerDb dealerDb;
        private readonly IScoreDb scoreDb;
        private readonly ITermDb termDb;

        public IncentiveCampaignApl(IIncentiveCampaignDb incentiveCampaignDb, 
            IDealershipDb dealershipDb, 
            IDealerDb dealerDb, 
            IScoreDb scoreDb, 
            ITermDb termDb)
        {
            this.incentiveCampaignDb = incentiveCampaignDb;            
            this.dealershipDb = dealershipDb;
            this.dealerDb = dealerDb;
            this.scoreDb = scoreDb;
            this.termDb = termDb;
        }

        public IncentiveCampaignApl()
        {
            incentiveCampaignDb = new IncentiveCampaignDb();
            //incentiveCampaignDb = new IncentiveCampaignCorporateDb();
            dealershipDb = new DealershipDb();
            dealerDb = new DealerDb();
            scoreDb = new ScoreDb();
            termDb = new TermDb();
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            //TODO incluir transaction 
            incentiveCampaign.CreationDate = DateTime.Now;
            incentiveCampaign.IsActive = true;
            incentiveCampaign.UserName = "RBRONZO";


            var campaign = incentiveCampaignDb.Create(incentiveCampaign);

            foreach(var d in incentiveCampaign.Dealerships)
            {
                var dealership = dealershipDb.Create(campaign.Id, d);
                campaign.Dealerships.Add(dealership);
            }
            
            return campaign;
        }

        /// <summary>
        /// Updates only the campaign. Dealerships has his own controller for services using campaign Id as parameter
        /// </summary>
        /// <param name="incentiveCampaign"></param>
        /// <returns></returns>
        public IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign)
        {
            var checkedCampaign = this.GetById(incentiveCampaign.Id);
            
            if (checkedCampaign != null)            
                incentiveCampaignDb.Update(incentiveCampaign);            
            else
                throw new Exception("Campaign.does.not.exists");

            return incentiveCampaign;
        }
        
        /// <summary>
        /// According to "On Cascade Delete" on database sctructure, 
        /// it ill be deleted the campaign, dealerships, dealers
        /// check if scores has this constraint. It's supposed to not have it;
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public bool Delete(int campaignId)
        {
            //Todo incluir na transaction
            return incentiveCampaignDb.Delete(campaignId);

        }

        public List<IncentiveCampaignEntity> GetAll()
        {
            return incentiveCampaignDb.ReadAll();
        }

        public IncentiveCampaignEntity GetById(int campaignId)
        {
            var campaign = incentiveCampaignDb.ReadById(campaignId);

            if(campaign != null)
            {
                campaign.Dealerships = dealershipDb.ReadByCampaign(campaignId);
                campaign.Terms = termDb.ReadByCampaign(campaignId);
            }

            return campaign;
        }

        public List<IncentiveCampaignEntity> GetByDealer(int dealerId)
        {
            return incentiveCampaignDb.ReadByDealer(dealerId);
        }

        public List<IncentiveCampaignEntity> GetByDealership(int dealershipId)
        {
            return incentiveCampaignDb.ReadByDealer(dealershipId);
        }

        public List<IncentiveCampaignEntity> GetManagerCampaigns(int dealershipId, int managerId)
        {
            var campaigns = this.GetByDealership(dealershipId);            
            foreach (var campaign in campaigns)
            {
                var user = dealerDb.ReadByIdAndCampaignId(managerId, campaign.Id);

                var dealership = dealershipDb.ReadById(dealershipId);

                user.Scores = scoreDb.ReadByDealerAndDealership(dealership.Id, user.Id);

                dealership.Dealers.Add(user);

                campaign.Dealerships.Add(dealership);                                
            }

            //N Campaigns >> 1 Dealerships >> 1 Dealer >> N Scores
            return campaigns;
        }

        public TermEntity UploadTerm(TermEntity term, int campaignId)
        {
            term.Id = termDb.Upload(term, campaignId);

            return term;
        }

        public TermEntity Download(int termId)
        {
            return termDb.Download(termId);
        }
    }
}
