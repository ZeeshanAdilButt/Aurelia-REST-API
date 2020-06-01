using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Core.Implementations
{
    public class HahnHTTPClientFactory : IHahnHTTPClientFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        public HahnHTTPClientFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetDataAsync<T>(string requestUri, string scheme = null, string schemeToken = null, string acceptLangauage = null) where T : class
        {
            T data = null;
            using (var client = _clientFactory.CreateClient("HahnClientFactory"))
            {
                // check if langauage avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(acceptLangauage) == false)
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLangauage));
                }

                // check if scheme avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(scheme) == false && string.IsNullOrEmpty(schemeToken) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, schemeToken);
                }


                var responseData = await client.GetStringAsync(requestUri);

                if (!string.IsNullOrWhiteSpace(responseData))
                {
                    data = JsonConvert.DeserializeObject<T>(responseData);
                }
            }
            return data;
        }
    }
}
