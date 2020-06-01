using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.Contracts.V1.Requests
{
    public class CreateApplicantRequest
    {
        //[Required(ErrorMessage = "Please provide a value for Name")]
        //[StringLength(80, MinimumLength = 5, ErrorMessage = "Name must be atleast 5 characters")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please provide a value for FamilyName")]
        //[StringLength(80, MinimumLength = 5, ErrorMessage = "FamilyName must be atleast 5 characters")]
        public string FamilyName { get; set; }

        //[Required(ErrorMessage = "Please provide a value for Address")]
        //[StringLength(500, MinimumLength = 10, ErrorMessage = "Address must be atleast 10 characters")]
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }

        //[Required(ErrorMessage = "Please provide a value for EmailAddress")]
        //[EmailAddress]
        //[DataType(DataType.EmailAddress, ErrorMessage = "EmailAddress is not valid")]
        public string EmailAddress { get; set; }

        //[Required(ErrorMessage = "Please provide a value for Age")]
        //[Range(20, 60, ErrorMessage = "The Age must be between 20 and 60")]
        public int Age { get; set; }
        public bool Hired { get; set; }

    }
}
