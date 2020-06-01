using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.May2020.Core.Implementations;
using Hahn.ApplicatonProcess.May2020.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Web.Installer
{
    public class MVCInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();

            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add<ValidationFilter>();
                })
                 .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                 .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHttpClient("HahnClientFactory");
            services.AddSingleton<IHahnHTTPClientFactory, HahnHTTPClientFactory>();

            //swagger registration
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn May 2020", Version = "v1" });

                //var security = new Dictionary<string, IEnumerable<string>> {
                //    { "Bearer", new string[0]}
                //};

                //x.AddSecurityDefinition(name:"Bearer", new APIKeyScheme {
                //})

            });

        }
    }
}
