using IncentiveCampaign.CorporateRepository;
using IncentiveCampaign.Domain.Dealer;
using IncentiveCampaign.Domain.Dealership;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Domain.Interface.Apl;
using IncentiveCampaign.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Apl
{
    
    public interface IDealershipApl
    {
        bool Register(int campaignId, DealershipEntity dealership);

        bool Delete(int campaignId, int dealershipId);

        List<DealershipEntity> GetByCampaign(int campaignId);

        List<DealershipEntity> GetByDealer(int dealerId);

        DealershipEntity GetById(int dealershipId);

        bool UploadAgreementLetter(int campaignId, int dealershipId, AgreementLetter agreementLetter);

        AgreementLetter DownloadAgreementLetter(int campaignId, int dealershipId);

        void Check(int dealerId);
    }

    public class DealershipApl : IDealershipApl
    {
        private readonly IDealershipDb dealershipDb;        
        private readonly IIncentiveCampaignApl incentiveCampaignApl;
        private readonly IDealerApl dealerApl;        

        public DealershipApl(IDealershipDb dealershipDb,
            IIncentiveCampaignApl incentiveCampaignApl,
            IDealerApl dealerApl)
        {
            this.dealershipDb = dealershipDb;
            this.incentiveCampaignApl = incentiveCampaignApl;
            this.dealerApl = dealerApl;
        }

        public DealershipApl()
        {
            this.dealershipDb = new DealershipCorporateDb();
            this.dealerApl = new DealerApl();            
        }

        public bool Register(int campaignId, DealershipEntity dealership)
        {            
            var relatedCampaign = incentiveCampaignApl.GetById(campaignId);

            var dealershipsOnCampaign = dealershipDb.ReadByCampaign(campaignId);

            if (dealershipsOnCampaign.Contains(dealership))
                throw new Exception("Dealership.Already.Registered");

            if (relatedCampaign == null)
                throw new Exception("Campaign.Does.Not.Exists");
            
            return dealershipDb.Register(campaignId, dealership);                            
        }

        public bool Delete(int campaignId, int dealershipId)
        {
            var relatedCampaign = incentiveCampaignApl.GetById(campaignId);

            if (relatedCampaign == null)
                throw new Exception("Campaign.Does.Not.Exists");
            
            return dealershipDb.Delete(campaignId, dealershipId);                            
        }

        public List<DealershipEntity> GetByCampaign(int campaignId)
        {
            return this.dealershipDb.ReadByCampaign(campaignId);
        }

        public List<DealershipEntity> GetByDealer(int dealerId)
        {            
            dealerApl.Check(dealerId);

            return this.dealershipDb.ReadByDealer(dealerId);
        }

        public DealershipEntity GetById(int dealershipId)
        {
            return dealershipDb.ReadById(dealershipId);
        }

        public bool UploadAgreementLetter(int campaignId, int dealershipId, AgreementLetter agreementLetter)
        {
            return dealershipDb.UploadAgreementLetter(dealershipId, agreementLetter);
        }

        public AgreementLetter DownloadAgreementLetter(int campaignId, int dealershipId)
        {
            return dealershipDb.DownloadAgreementLetter(campaignId, dealershipId);
        }


        public void Check(int dealershipId)
        {
            var dealership = dealershipDb.ReadById(dealershipId);

            if (!dealership.IsRegistered())
                throw new Exception("UnRegistered.IncentiveCampaign.Dealership.Dealer");            
        }

        public void Check(int dealershipId, int campaignId)
        {
            var campaign = incentiveCampaignApl.GetById(campaignId);

            var dealership = dealershipDb.ReadById(dealershipId);

            if (dealership.IsValidByAgreementLetter(campaign.AgreementLetterNeeded))
                throw new Exception("AgreementLetter.Not.Sent");
        }

    }
}
