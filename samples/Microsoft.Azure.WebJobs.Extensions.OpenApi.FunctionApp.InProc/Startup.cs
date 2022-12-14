using System;

using AutoFixture;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.FunctionApp.InProc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using static Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations.DefaultOpenApiConfigurationOptions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Microsoft.Azure.WebJobs.Extensions.OpenApi.FunctionApp.InProc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(new Fixture())
                            .AddSingleton<IOpenApiConfigurationOptions>(_ =>
                            {
                                var options = new OpenApiConfigurationOptions
                                {
                                    Info = new OpenApiInfo
                                    {
                                        Version = GetOpenApiDocVersion(),
                                        Title = $"{GetOpenApiDocTitle()} (Injected)",
                                        Description = GetOpenApiDocDescription(),
                                        TermsOfService = new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
                                        Contact = new OpenApiContact
                                        {
                                            Name = "Enquiry",
                                            Email = "azfunc-openapi@microsoft.com",
                                            Url = new Uri("https://github.com/Azure/azure-functions-openapi-extension/issues"),
                                        },
                                        License = new OpenApiLicense
                                        {
                                            Name = "MIT",
                                            Url = new Uri("http://opensource.org/licenses/MIT"),
                                        }
                                    },
                                    Servers = GetHostNames(),
                                    OpenApiVersion = GetOpenApiVersion(),
                                    IncludeRequestingHostName = IsFunctionsRuntimeEnvironmentDevelopment(),
                                    ForceHttps = IsHttpsForced(),
                                    ForceHttp = IsHttpForced(),
                                };

                                return options;
                            });
        }
    }
}
