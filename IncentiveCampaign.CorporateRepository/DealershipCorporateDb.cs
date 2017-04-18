using IncentiveCampaign.Domain.Dealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.CorporateRepository
{
    public class DealershipCorporateDb : IDealershipDb
    {
        internal readonly IDatabaseConnector connector;

        public DealershipCorporateDb()
        {
            this.connector = new DatabaseConnector();
            this.connector.Database = DbNames.Database.BmbDigital;
        }

        public DealershipCorporateDb(IDatabaseConnector connector)
        {
            this.connector = connector;
            this.connector.Database = DbNames.Database.BmbDigital;
        }

        public bool Register(int campaignId, DealershipEntity dealership)
        {
            try
            {
                this.connector.Procedure = "spr_digit_ins_refac_campa_incen_conce";

                this.connector.AddParameter("num_campa_incen", campaignId);
                this.connector.AddParameter("num_entid_conce", dealership.Id);
                this.connector.AddParameter("ind_carta_acord", dealership.AgreementLetterSent);

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

        public bool Delete(int campaignId, int dealershipId)
        {
            try
            {
                this.connector.Procedure = "spr_digit_del_refac_campa_incen_conce";

                this.connector.AddParameter("num_campa_incen", campaignId);
                this.connector.AddParameter("num_entid_conce", dealershipId);                

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

        // usado para montar o objeto de campanha ao editar uma campanha
        public List<DealershipEntity> ReadByCampaign(int campaignId)
        {
            try
            {
                this.connector.Procedure = "spr_digit_ler_refat_campa_incen_conce_por_campa";
                this.connector.AddParameter("@num_campa_incen", campaignId);

                using (var reader = this.connector.ExecuteReader())
                {
                    var campaigns = new List<DealershipEntity>();

                    while (reader.Read())
                    {
                        var c = new DealershipEntity
                        {
                            Id = Convert.ToInt32(reader["num_entid_conce"]),
                            Name = reader["nom_campa_incen"].ToString(),
                            Cnpj = reader["cnpj_cpf_entid_conce"].ToString(),
                            AgreementLetterSent = Convert.ToBoolean(reader["ind_carta_acord"])
                            //Dealers                            
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

        //onde é usado?
        public List<DealershipEntity> ReadByDealer(int dealerId)
        {
            try
            {
                this.connector.Procedure = "spr_digit_ler_refat_campa_incen_conce_por_entid";
                this.connector.AddParameter("@num_campa_incen_usuar", dealerId);

                using (var reader = this.connector.ExecuteReader())
                {
                    var campaigns = new List<DealershipEntity>();

                    while (reader.Read())
                    {
                        var c = new DealershipEntity
                        {
                            Id = Convert.ToInt32(reader["num_entid_conce"]),
                            Name = reader["nom_conce"].ToString(),
                            Cnpj = reader["num_cnpj_cpf"].ToString(),
                            //AgreementLetterSent = Convert.ToBoolean(reader["cnpj_cpf_entid_conce"])
                            //Dealers                            
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
        
        public DealershipEntity ReadById(int dealershipId)
        {
            try
            {
                this.connector.Procedure = "spr_digit_ler_refat_campa_incen_conce_por_id";
                this.connector.AddParameter("@num_campa_incen_usuar", dealershipId);

                using (var reader = this.connector.ExecuteReader())
                {
                    var campaigns = new DealershipEntity();

                    while (reader.Read())
                    {
                        var c = new DealershipEntity
                        {
                            Id = Convert.ToInt32(reader["num_campa_incen"]),
                            Name = reader["nom_campa_incen"].ToString(),
                            Cnpj = reader["cnpj_cpf_entid_conce"].ToString(),
                            AgreementLetterSent = Convert.ToBoolean(reader["cnpj_cpf_entid_conce"])
                            //Dealers                            
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
