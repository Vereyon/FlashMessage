using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vereyon.Web
{
    public static class FlashMessageExtensions
    {

        /// <summary>
        /// Registeres flash message service and adds services required for flash message to work.
        /// </summary>
        /// <param name="services"></param>
        public static void AddFlashMessage(this IServiceCollection services)
        {

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IFlashMessageSerializer, JsonFlashMessageSerializer>();
            services.AddScoped<IFlashMessage, FlashMessage>();
        }
    }
}
