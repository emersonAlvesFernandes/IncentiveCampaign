using IncentiveCampaign.Domain.IncentiveCampaign;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public interface IIncentiveCampaignDb
    {
        IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign);

        IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign);

        bool Delete(int campaignId);

        List<IncentiveCampaignEntity> ReadAll();

        IncentiveCampaignEntity ReadById(int Id);

        List<IncentiveCampaignEntity> ReadByDealer(int dealerId);
        
    }

    public class IncentiveCampaignDb : RepositoryBase, IIncentiveCampaignDb
    {
        public IncentiveCampaignDb(): base()
        {
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            var sql = @"INSERT INTO tbl_campa_incen 
(
nom_campa_incen,
dat_inici_vigen,
dat_final_vigen,
ind_neces_carta_acord,
ind_ativo
)
VALUES
(
@nom_campa_incen,
@dat_inici_vigen,
@dat_final_vigen,
@ind_neces_carta_acord,
@ind_ativo
)

SELECT Scope_Identity() as num_campa_incen
FROM tbl_campa_incen WITH(NOLOCK)		

";
            this.connection = new SqlConnection(connectionstring);
            this.OpenConnection();

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@nom_campa_incen", incentiveCampaign.Name);
                cmd.Parameters.AddWithValue("@dat_inici_vigen", incentiveCampaign.StartDate);
                cmd.Parameters.AddWithValue("@dat_final_vigen", incentiveCampaign.EndDate);
                cmd.Parameters.AddWithValue("@ind_neces_carta_acord", incentiveCampaign.AgreementLetterNeeded);
                cmd.Parameters.AddWithValue("@ind_ativo", incentiveCampaign.IsActive);

                //cmd.ExecuteNonQuery();
                var datareader = cmd.ExecuteReader();
                while(datareader.Read())
                {
                    incentiveCampaign.Id = Convert.ToInt32(datareader["num_campa_incen"]);
                }                    
            }

            return incentiveCampaign;
        }

        public IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int campaignId)
        {
            throw new NotImplementedException();
        }

        public List<IncentiveCampaignEntity> ReadAll()
        {
            var collection = new List<IncentiveCampaignEntity>();

            var query = @"SELECT * FROM TBL_CAMPA_INCEN";

            base.connection = new SqlConnection(connectionstring);
            this.OpenConnection();

            using (var cmd = new SqlCommand(query, connection))
            {
                var datareader = cmd.ExecuteReader();

                while(datareader.Read())
                {
                    var obj = new IncentiveCampaignEntity
                    {
                        Id = Convert.ToInt32(datareader["num_campa_incen"]),
                        Name = datareader["nom_campa_incen"].ToString(),
                        StartDate = Convert.ToDateTime(datareader["dat_inici_vigen"]),
                        EndDate = Convert.ToDateTime(datareader["dat_final_vigen"]),
                        AgreementLetterNeeded = Convert.ToBoolean(datareader["ind_neces_carta_acord"]),
                        IsActive = Convert.ToBoolean(datareader["ind_ativo"]),
                    };

                    collection.Add(obj);
                }
                return collection;
            }

        }

        public List<IncentiveCampaignEntity> ReadByDealer(int dealerId)
        {
            throw new NotImplementedException();
        }

        public List<IncentiveCampaignEntity> GetByDealership(int dealerId)
        {
            throw new NotImplementedException();
        }

        public IncentiveCampaignEntity ReadById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
