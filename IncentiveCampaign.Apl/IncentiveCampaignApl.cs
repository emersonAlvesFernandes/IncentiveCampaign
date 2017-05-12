using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Interface.Apl;
using IncentiveCampaign.Domain.Term;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        void Check(int campaignId);
    }


    public class IncentiveCampaignApl : IIncentiveCampaignApl
    {
        private readonly IIncentiveCampaignDb incentiveCampaignDb;
        
        //private readonly IDealershipApl dealershipApl;
        //private readonly IDealerApl dealerApl;
        //private readonly IScoreApl scoreApl;
        //private readonly ITermApl termApl;

        public IncentiveCampaignApl(IIncentiveCampaignDb incentiveCampaignDb
            //IDealershipApl dealershipApl,
            //IDealerApl dealerApl,
            //IScoreApl scoreApl,
            //ITermApl termApl
            )
        {
            this.incentiveCampaignDb = incentiveCampaignDb;            
            //this.dealershipApl = dealershipApl;
            //this.dealerApl = dealerApl;
            //this.scoreApl = scoreApl;
            //this.termApl = termApl;
        }

        public IncentiveCampaignApl()
        {
            //this.incentiveCampaignDb = new IncentiveCampaignCorporateDb();            
            this.incentiveCampaignDb = new IncentiveCampaignDb();

            this.dealershipApl = new DealershipApl();
            this.dealerApl = new DealerApl();
            this.termApl = new TermApl();
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            //Check if name already exists
            var allCampaigns = incentiveCampaignDb.ReadAll();
            var names = allCampaigns.Select(x => x.Name.ToUpper());
            if (names.Contains(incentiveCampaign.Name))
                throw new Exception("Already.Exists.Campaign.With.This.Name");
            
            return this.incentiveCampaignDb.Create(incentiveCampaign);
        }

        /// <summary>
        /// Updates only the campaign. Dealerships has his own controller for services using campaign Id as parameter
        /// </summary>
        /// <param name="incentiveCampaign"></param>
        /// <returns></returns>
        public IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign)
        {
            var checkedCampaign = this.GetById(incentiveCampaign.Id);

            if (checkedCampaign == null)
                throw new Exception("Campaign.does.not.exists");
            
            this.incentiveCampaignDb.Update(incentiveCampaign);                                        

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
            return incentiveCampaignDb.Delete(campaignId);
        }

        public List<IncentiveCampaignEntity> GetAll()
        {
            return incentiveCampaignDb.ReadAll();
        }

        public IncentiveCampaignEntity GetById(int campaignId)
        {
            var campaign = incentiveCampaignDb.ReadById(campaignId);

            if (campaign == null)
                return null;
                                                
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
                //var user = dealerDb.ReadByIdAndCampaignId(managerId, campaign.Id);
                var user = this.dealerApl.GetByIdAndCampaignId(managerId, campaign.Id);

                //var dealership = dealershipDb.ReadById(dealershipId);
                var dealership = this.dealershipApl.GetById(dealershipId);

                //user.Scores = scoreDb.ReadByDealerAndDealership(dealership.Id, user.Id);
                user.Scores = this.scoreApl.GetByDealerAndDealership(dealership.Id, user.Id);

                dealership.Dealers.Add(user);

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

