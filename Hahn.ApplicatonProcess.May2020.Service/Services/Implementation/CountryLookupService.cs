using Hahn.ApplicatonProcess.May2020.Core.Implementations;
using Hahn.ApplicatonProcess.May2020.Domain.Models.ServiceModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Service.Services.Implementation
{
    public class CountryLookupService : ICountryLookupService
    {
        private readonly IHahnHTTPClientFactory clientFactory;
        private readonly IConfiguration _configuration;

        public CountryLookupService(IHahnHTTPClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            _configuration = configuration;
        }

        public CountryLookupService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        //public HahnHTTPClientFactory clientFactory { get; }

        public async Task<bool> IsValidCountry(string countryName)
        {
            //Todo call: http Client
            List<CountryResponse> response = await clientFactory.GetDataAsync<List<CountryResponse>>(_configuration["ServiceUrls:CountryAPI"]);

            var isExist = response.FirstOrDefault(x => x.Name == countryName);

            if (isExist != null)
                return true;

            return false;
        }
    }
}
