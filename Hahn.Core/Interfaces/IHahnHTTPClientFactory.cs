using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Core.Implementations
{
    public interface IHahnHTTPClientFactory
    {
        Task<T> GetDataAsync<T>(string requestUri, string scheme = null, string schemeToken = null, string acceptLangauage = null) where T : class;
    }
}
