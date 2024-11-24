using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Infrastructure.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                await HandleResponseAsync(context.HttpContext, "ApiKey was not provided");
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKeys = appSettings.GetSection(APIKEYNAME)
                                       .GetChildren()
                                       .Select(x => x.Value)
                                       .ToArray();

            if (apiKeys != null && !apiKeys.Any(key => key.Equals(extractedApiKey)))
            {
                await HandleResponseAsync(context.HttpContext, "AppId was not provided");
                return;
            }

            await next();
        }
        private Task HandleResponseAsync(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseBase<string>(null, context.Response.StatusCode, message, "ERROR")));
        }
    }
}
