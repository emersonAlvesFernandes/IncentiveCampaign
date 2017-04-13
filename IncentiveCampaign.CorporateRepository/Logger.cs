using BMB.Corporativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveCampaign.CorporateRepository
{
    public interface ILogger
    {
        void Write(string message);

        void Write(Exception exception);

        void Write(string message, Exception exception);
    }

    internal class Logger : ILogger
    {
        public void Write(string message)
        {
            LogErro.Gravar(message);
        }

        public void Write(Exception exception)
        {
            LogErro.Gravar(exception);
        }

        public void Write(string message, Exception exception)
        {
            LogErro.Gravar(message, exception);
        }
    }
}
