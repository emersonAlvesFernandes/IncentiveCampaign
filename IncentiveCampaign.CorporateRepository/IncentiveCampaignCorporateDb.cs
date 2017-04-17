using IncentiveCampaign.Domain.IncentiveCampaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.CorporateRepository
{
    public class IncentiveCampaignCorporateDb : IIncentiveCampaignDb
    {
        internal readonly IDatabaseConnector connector;

        public IncentiveCampaignCorporateDb()
        {
            this.connector = new DatabaseConnector();
            this.connector.Database = DbNames.Database.BmbDigital;
        }

        public IncentiveCampaignCorporateDb(IDatabaseConnector connector)
        {
            this.connector = connector;
            this.connector.Database = DbNames.Database.BmbDigital;
        }

        public IncentiveCampaignEntity Create(IncentiveCampaignEntity incentiveCampaign)
        {
            try
            {                
                this.connector.Procedure = "spr_digit_ins_refac_campa_incen";

                //this.connector.AddParameter("num_campa_incen", incentiveCampaign.Id);
                this.connector.AddParameter("nom_campa_incen", incentiveCampaign.Name);
                this.connector.AddParameter("dat_inici_vigen", incentiveCampaign.StartDate);
                this.connector.AddParameter("dat_final_vigen", incentiveCampaign.EndDate);
                this.connector.AddParameter("ind_ativo", incentiveCampaign.IsActive);
                this.connector.AddParameter("dat_situa_regis", incentiveCampaign.CreationDate);
                this.connector.AddParameter("cod_user", incentiveCampaign.UserName);
                this.connector.AddParameter("ind_neces_carta_acord", incentiveCampaign.AgreementLetterNeeded);

                using (var reader = this.connector.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        incentiveCampaign.Id = Convert.ToInt32(reader["num_campa_incen"]);
                        return incentiveCampaign;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.connector.Dispose();
            }
        }

        public IncentiveCampaignEntity Update(IncentiveCampaignEntity incentiveCampaign)
        {
            try
            {
                this.connector.Procedure = "spr_digit_upd_refac_campa_incen";

                this.connector.AddParameter("num_campa_incen", incentiveCampaign.Id);
                this.connector.AddParameter("nom_campa_incen", incentiveCampaign.Name);
                this.connector.AddParameter("dat_inici_vigen", incentiveCampaign.StartDate);
                this.connector.AddParameter("dat_final_vigen", incentiveCampaign.EndDate);
                this.connector.AddParameter("ind_ativo", incentiveCampaign.IsActive);
                this.connector.AddParameter("dat_situa_regis", incentiveCampaign.CreationDate);
                this.connector.AddParameter("cod_user", incentiveCampaign.UserName);
                this.connector.AddParameter("ind_neces_carta_acord", incentiveCampaign.AgreementLetterNeeded);

                using (var reader = this.connector.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        incentiveCampaign.Id = Convert.ToInt32(reader["num_campa_incen"]);
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
                this.connector.Dispose();
            }
        }

        public bool Delete(int campaignId)
        {
            try
            {                
                this.connector.Procedure = "spr_digit_del_refac_campa_incen";
                this.connector.AddParameter("num_campa_incen", campaignId);
                this.connector.ExecuteReader();
                return true;
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.connector.Dispose();
            }
        }

        public List<IncentiveCampaignEntity> ReadAll()
        {
            try
            {                                       
                this.connector.Procedure = "spr_digit_ler_refat_campa_incen";
                
                using (var reader = this.connector.ExecuteReader())
                {
                    var campaigns = new List<IncentiveCampaignEntity>();

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
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                this.connector.Dispose();
            }
        }

        public List<IncentiveCampaignEntity> ReadByDealer(int dealerId)
        {
            try
            {                
                this.connector.Procedure = "spr_digit_ler_refat_campa_incen_por_dealr";
                this.connector.AddParameter("@num_campa_incen_usuar", dealerId);

                using (var reader = this.connector.ExecuteReader())
                {
                    var campaigns = new List<IncentiveCampaignEntity>();

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
                this.connector.Dispose();
            }
        }

        public IncentiveCampaignEntity ReadById(int id)
        {
            try
            {
                this.connector.Procedure = "spr_digit_ler_refat_campa_incen_por_id";
                this.connector.AddParameter("@num_campa_incen_usuar", id);

                using (var reader = this.connector.ExecuteReader())
                {                    
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
                this.connector.Dispose();
            }
        }

        
    }
}
