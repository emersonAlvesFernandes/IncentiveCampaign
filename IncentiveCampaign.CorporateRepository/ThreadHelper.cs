using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IncentiveCampaign.CorporateRepository
{
    public static class ThreadHelper
    {
        public static void Execute(ThreadStart action, string cultureName = "en-US")
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (string.IsNullOrWhiteSpace(cultureName))
                throw new ArgumentException("Cannot be null or empty.", nameof(action));

            var cultureInfo = new CultureInfo(cultureName);

            var thread = new Thread(action);
            thread.CurrentCulture = cultureInfo;

            thread.Start();
        }
    }
}
