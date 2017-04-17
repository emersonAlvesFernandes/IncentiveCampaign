using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.CorporateRepository
{
    public class TermCorporateDb : ITermDb
    {
        internal readonly IDatabaseConnector connector;

        public TermCorporateDb()
        {
            this.connector = new DatabaseConnector();
            this.connector.Database = DbNames.Database.BmbDigital;
        }

        public TermEntity Register(int incentiveCampaignId, TermEntity term)
        {
            try
            {
                this.connector.Procedure = "spr_digit_ins_campa_incen_termo";

                this.connector.AddParameter("num_campa_incen", incentiveCampaignId);
                this.connector.AddParameter("nom_campa_incen_termo", term.Name);
                this.connector.AddParameter("dat_publi", DateTime.Now);
                this.connector.AddParameter("tip_exten", term.Extention);
                this.connector.AddParameter("val_docum", term.Contents);
                this.connector.AddParameter("num_bytes", term.TotalBytes);
                this.connector.AddParameter("cod_user", term.TotalBytes);

                using (var reader = this.connector.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        term.Id = Convert.ToInt32(reader["num_campa_incen"]);
                        return term;
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

        public bool Delete(int termId)
        {
            try
            {
                this.connector.Database = DbNames.Database.BmbDigital;
                this.connector.Procedure = "spr_digit_del_campa_incen_termo";

                this.connector.AddParameter("num_campa_incen_termo", termId);
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

        public TermEntity Download(int documentId)
        {
            try
            {
                this.connector.Database = DbNames.Database.BmbDigital;
                this.connector.Procedure = "spr_digit_ler_campa_incen_termo";
                this.connector.AddParameter("num_campa_incen_termo", documentId);

                using (var reader = this.connector.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new TermEntity
                        {
                            Id = Convert.ToInt32(reader["num_campa_incen_termo"]),
                            Name = reader["nom_campa_incen_termo"].ToString(),                            
                            //CreationDate = reader["dat_publi"].ToDateTime(),
                            Extention = reader["tip_exten"].ToString(),
                            Contents = (byte[])reader["val_docum"],                            
                            //UserId = reader["cod_user"].ToString()
                            TotalBytes = Convert.ToInt64(reader["num_bytes"])
                        };

                        return c;
                    }
                }
                return null;
            }
            finally
            {
                this.connector.Dispose();
            }
        }

        public TermEntity ReadByCampaign(int campaignId)
        {
            throw new NotImplementedException();
        }
    }
}
