using IncentiveCampaign.Domain.Dealership;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncentiveCampaign.Domain.Dealer;
using System.Data.SqlClient;

namespace IncentiveCampaign.Repository
{

    public class DealershipDb : RepositoryBase, IDealershipDb
    {
        public DealershipDb()
        {
            this.connection = new SqlConnection(connectionstring);
            this.OpenConnection();
        }

        public DealershipEntity Create(int campaignId, DealershipEntity incentiveCampaign)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int campaignId, int dealershipId)
        {
            throw new NotImplementedException();
        }

        public DealershipEntity ReadById(int dealershipId)
        {
            throw new NotImplementedException();
        }

        public List<DealershipEntity> ReadByCampaign(int campaign)
        {
            throw new NotImplementedException();
        }

        public List<DealershipEntity> ReadByDealer(int dealerId)
        {
            throw new NotImplementedException();
        }

        //TODO: Testar
        public bool Register(int campaignId, DealershipEntity dealership)
        {
            try
            {
                var sql = "spr_digit_ins_campa_incen_conce";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@num_campa_incen", campaignId);
                    cmd.Parameters.AddWithValue("@num_entid_conce", dealership.Id);

                    var datareader = cmd.ExecuteReader();                    
                }

                return true;

            }
            catch(Exception ex)
            {
                throw ex;
            }                
        }

        public bool UploadAgreementLetter(int dealershipId, AgreementLetter agreementLetter)
        {
            throw new NotImplementedException();
        }

        public AgreementLetter DownloadAgreementLetter(int campaignId, int dealershipId)
        {
            throw new NotImplementedException();
        }
    }
}
