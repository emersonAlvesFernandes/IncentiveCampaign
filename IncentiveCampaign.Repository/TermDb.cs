using IncentiveCampaign.Domain.Term;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.Repository
{
    public class TermDb : RepositoryBase, ITermDb
    {
        public TermDb()
        {
            this.connection = new SqlConnection(connectionstring);
            this.OpenConnection();
        }

        public bool Delete(int termId)
        {
            throw new NotImplementedException();
        }

        public TermEntity Download(int TermId)
        {
            throw new NotImplementedException();
        }

        public List<TermEntity> ReadByCampaign(int campaignId)
        {
            throw new NotImplementedException();
        }

        public bool Register(int incentiveCampaignId, TermEntity term, string codUser)
        {
            try
            {
                var sql = "spr_digit_ins_campa_incen_termo";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@num_campa_incen", incentiveCampaignId);
                    cmd.Parameters.AddWithValue("@nom_campa_incen_termo]", term.Name);
                    cmd.Parameters.AddWithValue("@dat_publi]", term.PublishDate);
                    cmd.Parameters.AddWithValue("@tip_exten]", term.Extention);
                    cmd.Parameters.AddWithValue("@val_docum]", term.Contents);
                    cmd.Parameters.AddWithValue("@num_bytes]", term.TotalBytes);
                    cmd.Parameters.AddWithValue("@cod_user]", codUser);

                    var datareader = cmd.ExecuteReader();
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Upload(int campaignId, TermEntity term)
        {
            throw new NotImplementedException();
        }        
    }
}
