using BancoDaimlerChrysler.Corporativo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.CorporateRepository
{
    public interface IDatabaseConnector : IDisposable
    {
        string Database { get; set; }

        string Procedure { get; set; }

        SqlTransaction Transaction { get; set; }

        SqlConnection OpenConnection();

        void AddParameter(string name, object value);

        int ExecuteNonQuery();

        //TType ExecuteSingle<TType>();

        IDataReader ExecuteReader();

        DataSet ExecuteDataSet();
    }

    public class DatabaseConnector : IDatabaseConnector
    {
        private string database;

        private string procedure;

        private readonly Connector connector;

        private readonly Dictionary<string, object> parameters;

        private readonly ILogger logger;

        private SqlTransaction transaction;

        public DatabaseConnector()
        {
            this.connector = new Connector();
            this.parameters = new Dictionary<string, object>();
            this.logger = new Logger();
        }

        public DatabaseConnector(ILogger logger)
        {
            this.connector = new Connector();
            this.parameters = new Dictionary<string, object>();
            this.logger = logger;
        }

        public string Database
        {
            get { return this.database; }
            set
            {
                this.database = value;
                this.connector.BancoDeDados = value;
            }
        }

        public string Procedure
        {
            get { return this.procedure; }
            set
            {
                this.procedure = value;
                this.connector.NomeProcedure = value;
                this.ClearParameters();
            }
        }

        public SqlTransaction Transaction
        {
            get { return this.transaction; }
            set
            {
                this.transaction = value;
                this.connector.Transacao = value;
            }
        }

        public SqlConnection OpenConnection()
        {
            return this.connector.AbrirConexao();
        }

        public void AddParameter(string name, object value)
        {
            this.parameters.Add(name, value);
            this.connector.AdicionaParametro(name, value);
        }

        private void ClearParameters()
        {
            this.connector.LimpaParametros();
            this.parameters.Clear();
        }

        public int ExecuteNonQuery()
        {
            return this.Log(() =>
            {
                return this.connector.ExecuteNoQuery();
            });
        }

        //public TType ExecuteSingle<TType>()
        //{
        //    return this.Log(() =>
        //    {
        //        var dataSet = this.connector.ExecuteDataSet();

        //        if (dataSet.Tables.Count == 0)
        //            return default(TType);

        //        if (dataSet.Tables[0].Rows.Count == 0)
        //            return default(TType);

        //        var value = dataSet.Tables[0].Rows[0][0];
        //        var castedValue = value.TryCast<TType>();

        //        return castedValue;
        //    });
        //}

        public IDataReader ExecuteReader()
        {
            return this.Log(() =>
            {
                return this.connector.ExecuteReader();
            });
        }

        public DataSet ExecuteDataSet()
        {
            return this.Log(() =>
            {
                return this.connector.ExecuteDataSet();
            });
        }

        private TType Log<TType>(Func<TType> function)
        {
            try
            {
#if DEBUG
                ThreadHelper.Execute(() => this.LogTrace());
#endif
                return function();
            }
            catch (Exception exception)
            {
                ThreadHelper.Execute(() => this.LogError(exception));
                throw;
            }
        }

        private void LogTrace()
        {
            var message = new StringBuilder();
            message.AppendLine("===============");
            message.AppendLine("TRACE:");
            message.AppendLine(this.ToString());
            message.AppendLine();
            this.logger.Write(message.ToString());
        }

        private void LogError(Exception exception)
        {
            var message = new StringBuilder();
            message.AppendLine("===============");
            message.AppendLine("ERROR:");
            message.AppendLine(this.ToString());
            message.AppendLine();
            message.AppendLine(exception.ToString());
            this.logger.Write(message.ToString());
        }

        public override string ToString()
        {
            var message = new StringBuilder();
            message.AppendLine($"EXEC {this.database}..{this.procedure}");

            foreach (var item in this.parameters)
            {
                string paramName = item.Key;
                string paramValue = item.Value == null ? "NULL" : $"'{item.Value}'";

                message.AppendLine($"\t{paramName} = {paramValue} ,");
            }

            message = message.Remove(message.Length - 3, 3);
            return message.ToString();
        }

        public void Dispose()
        {
            this.connector?.Dispose();
        }
    }
}
