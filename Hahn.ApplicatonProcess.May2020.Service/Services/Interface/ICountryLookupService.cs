using Hahn.ApplicatonProcess.May2020.Core.Implementations;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Service.Services.Implementation
{
    public interface ICountryLookupService
    {
        Task<bool> IsValidCountry(string countryName);
    }
}