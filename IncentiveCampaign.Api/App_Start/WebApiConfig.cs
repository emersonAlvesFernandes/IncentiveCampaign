using FluentValidation.WebApi;
using IncentiveCampaign.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IncentiveCampaign.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelStateFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi_",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //SwaggerConfig.Register();
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
