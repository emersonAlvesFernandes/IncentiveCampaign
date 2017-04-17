using IncentiveCampaign.Apl;
using IncentiveCampaign.Domain.IncentiveCampaign;
using IncentiveCampaign.Repository;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace IncentiveCampaign.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IIncentiveCampaignApl, IncentiveCampaignApl>();

            container.RegisterType<IIncentiveCampaignDb, IncentiveCampaignDb>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}