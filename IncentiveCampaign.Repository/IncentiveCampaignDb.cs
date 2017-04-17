using IncentiveCampaign.Domain.IncentiveCampaign;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{

    public class IncentiveCampaignDb : RepositoryBase, IIncentiveCampaignDb
    {
        public IncentiveCampaignDb(): base()
        {
            this.connection = new SqlConnection(connectionstring);
            this.OpenConnection();
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            try
            {
                #region query
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
                #endregion

                sql = "spr_digit_ins_refac_campa_incen";
                
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@nom_campa_incen", incentiveCampaign.Name);
                    cmd.Parameters.AddWithValue("@dat_inici_vigen", incentiveCampaign.StartDate);
                    cmd.Parameters.AddWithValue("@dat_final_vigen", incentiveCampaign.EndDate);
                    cmd.Parameters.AddWithValue("@ind_ativo", incentiveCampaign.IsActive);
                    cmd.Parameters.AddWithValue("@dat_situa_regis", incentiveCampaign.CreationDate);
                    cmd.Parameters.AddWithValue("@cod_user", incentiveCampaign.UserName);
                    cmd.Parameters.AddWithValue("@ind_neces_carta_acord", incentiveCampaign.AgreementLetterNeeded);

                    //cmd.ExecuteNonQuery();
                    var datareader = cmd.ExecuteReader();
                    while (datareader.Read())
                    {
                        incentiveCampaign.Id = Convert.ToInt32(datareader["num_campa_incen"]);
                    }
                }

                return incentiveCampaign;
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign)
        {
            try
            {
                var procedure = "spr_digit_upd_refac_campa_incen";                

                using (var cmd = new SqlCommand(procedure, connection))
                {
                    cmd.Parameters.AddWithValue("num_campa_incen", incentiveCampaign.Id);
                    cmd.Parameters.AddWithValue("nom_campa_incen", incentiveCampaign.Name);
                    cmd.Parameters.AddWithValue("dat_inici_vigen", incentiveCampaign.StartDate);
                    cmd.Parameters.AddWithValue("dat_final_vigen", incentiveCampaign.EndDate);
                    cmd.Parameters.AddWithValue("ind_ativo", incentiveCampaign.IsActive);
                    cmd.Parameters.AddWithValue("dat_situa_regis", incentiveCampaign.CreationDate);
                    cmd.Parameters.AddWithValue("cod_user", incentiveCampaign.UserName);
                    cmd.Parameters.AddWithValue("ind_neces_carta_acord", incentiveCampaign.AgreementLetterNeeded);

                    //cmd.ExecuteNonQuery();
                    var datareader = cmd.ExecuteReader();
                    while (datareader.Read())
                    {
                        incentiveCampaign.Id = Convert.ToInt32(datareader["num_campa_incen"]);
                        return incentiveCampaign;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public bool Delete(int campaignId)
        {
            try
            {
                var procedure = "spr_digit_del_refac_campa_incen";

                using (var cmd = new SqlCommand(procedure, connection))
                {
                    cmd.Parameters.AddWithValue("num_campa_incen", campaignId);
                    cmd.ExecuteReader();
                    return true;
                }                                                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public List<IncentiveCampaignEntity> ReadAll()
        {
            
            //var query = @"SELECT * FROM TBL_CAMPA_INCEN";

            var procedure = "spr_digit_ler_refat_campa_incen";

            using (var cmd = new SqlCommand(procedure, connection))
            {
                var collection = new List<IncentiveCampaignEntity>();
                var reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    var obj = new IncentiveCampaignEntity
                    {
                        Id = Convert.ToInt32(reader["num_campa_incen"]),
                        Name = reader["nom_campa_incen"].ToString(),
                        StartDate = Convert.ToDateTime(reader["dat_inici_vigen"]),
                        EndDate = Convert.ToDateTime(reader["dat_final_vigen"]),
                        IsActive = Convert.ToBoolean(reader["ind_ativo"]),
                        CreationDate = Convert.ToDateTime(reader["dat_situa_regis"]),
                        AgreementLetterNeeded = Convert.ToBoolean(reader["ind_neces_carta_acord"])
                    };

                    collection.Add(obj);
                }
                return collection;
            }

        }

        public List<IncentiveCampaignEntity> ReadByDealer(int dealerId)
        {
            try
            {
                var procedure = "spr_digit_ler_refat_campa_incen_por_dealr";
                
                using (var cmd = new SqlCommand(procedure, connection))
                {
                    cmd.Parameters.AddWithValue("@num_campa_incen_usuar", dealerId);

                    var campaigns = new List<IncentiveCampaignEntity>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var c = new IncentiveCampaignEntity
                        {
                            Id = Convert.ToInt32(reader["num_campa_incen"]),
                            Name = reader["nom_campa_incen"].ToString(),
                            StartDate = Convert.ToDateTime(reader["dat_inici_vigen"]),
                            EndDate = Convert.ToDateTime(reader["dat_final_vigen"]),
                            IsActive = Convert.ToBoolean(reader["ind_ativo"]),
                            CreationDate = Convert.ToDateTime(reader["dat_situa_regis"]),
                            AgreementLetterNeeded = Convert.ToBoolean(reader["ind_neces_carta_acord"])

                        };

                        campaigns.Add(c);
                    }

                    return campaigns;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.connection.Dispose();
            }
        }

        public List<IncentiveCampaignEntity> GetByDealership(int dealerId)
        {
            throw new NotImplementedException();
        }

        public IncentiveCampaignEntity ReadById(int id)
        {
            try
            {
                var procedure = "spr_digit_ler_refat_campa_incen_por_id";
                
                using (var cmd = new SqlCommand(procedure, connection))
                {
                    cmd.Parameters.AddWithValue("@num_campa_incen_usuar", id);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var c = new IncentiveCampaignEntity
                        {
                            Id = Convert.ToInt32(reader["num_campa_incen"]),
                            Name = reader["nom_campa_incen"].ToString(),
                            StartDate = Convert.ToDateTime(reader["dat_inici_vigen"]),
                            EndDate = Convert.ToDateTime(reader["dat_final_vigen"]),
                            IsActive = Convert.ToBoolean(reader["ind_ativo"]),
                            CreationDate = Convert.ToDateTime(reader["dat_situa_regis"]),
                            AgreementLetterNeeded = Convert.ToBoolean(reader["ind_neces_carta_acord"])

                        };
                        return c;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.connection.Dispose();
            }
        }
    }
}
