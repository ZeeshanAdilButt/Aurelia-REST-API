using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.Contracts
{
    public static class ApiRoutes
    {

        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Applicants
        {
            public const string GetAll = Base + "/applicants";
            public const string Get = Base + "/applicant/{applicantId}";
            public const string Update = Base + "/applicants";
            public const string Delete = Base + "/applicants";
            public const string Create = Base + "/applicants";

        }
    }
}
