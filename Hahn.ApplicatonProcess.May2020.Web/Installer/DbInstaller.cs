using Hahn.ApplicatonProcess.May2020.Data.Data;
using Hahn.ApplicatonProcess.May2020.Service.Services.Implementation;
using Hahn.ApplicatonProcess.May2020.Service.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicatonProcess.May2020.Web.Installer
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "Hahn"));

            services.AddScoped<IApplicantService, ApplicantService>();
            
            //todo: externalize
            services.AddScoped<ICountryLookupService, CountryLookupService>();


            //Todo: add authentication if time left
            //services.AddDefaultIdentity<IdentityUser>()
            //   .AddRoles<IdentityRole>()
            //   .AddEntityFrameworkStores<DataContext>();

        }
    }
}
