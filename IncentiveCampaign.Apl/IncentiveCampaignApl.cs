﻿using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.IncentiveCampaign;
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
    }


    public class IncentiveCampaignApl : IIncentiveCampaignApl
    {
        private readonly IIncentiveCampaignDb incentiveCampaignDb;
        //private readonly IDealershipDb dealershipDb;
        //private readonly IDealerDb dealerDb;
        //private readonly IScoreDb scoreDb;
        //private readonly ITermDb termDb;

        private readonly IDealershipApl dealershipApl;
        private readonly IDealerApl dealerApl;
        private readonly IScoreApl scoreApl;
        private readonly ITermApl termApl;

        public IncentiveCampaignApl(IIncentiveCampaignDb incentiveCampaignDb,
            IDealershipApl dealershipApl,
            IDealerApl dealerApl,
            IScoreApl scoreApl,
            ITermApl termApl)
        {
            this.incentiveCampaignDb = incentiveCampaignDb;
            //this.dealershipDb = dealershipDb;
            //this.dealerDb = dealerDb;
            //this.scoreDb = scoreDb;
            //this.termDb = termDb;
            this.dealershipApl = dealershipApl;
            this.dealerApl = dealerApl;
            this.scoreApl = scoreApl;
            this.termApl = termApl;
        }

        public IncentiveCampaignApl()
        {
            //incentiveCampaignDb = new IncentiveCampaignDb();
            this.incentiveCampaignDb = new IncentiveCampaignCorporateDb();
            //this.dealershipDb = new DealershipCorporateDb();
            //this.dealerDb = new DealerDb();
            //this.scoreDb = new ScoreDb();
            //this.termDb = new TermCorporateDb();
            
            this.dealershipApl = new DealershipApl();
            this.dealerApl = new DealerApl();
            this.termApl = new TermApl();
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {            
            using (var transaction = new TransactionScope())
            {
                incentiveCampaign.CreationDate = DateTime.Now;
                incentiveCampaign.IsActive = true;                

                incentiveCampaign = incentiveCampaignDb.Create(incentiveCampaign);

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
                        
            campaign.Dealerships = dealershipApl.GetByCampaign(campaignId);
            
            campaign.Terms = termApl.GetByCampaign(campaignId);
            
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
    }
}

