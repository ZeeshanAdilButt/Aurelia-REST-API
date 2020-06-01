using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicatonProcess.May2020.Web.Installer
{
    interface IInstaller
    {

        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
