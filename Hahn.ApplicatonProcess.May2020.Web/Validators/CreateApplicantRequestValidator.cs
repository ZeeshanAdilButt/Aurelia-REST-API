using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Web.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.Validators
{
    public class CreateApplicantRequestValidator :AbstractValidator<CreateApplicantRequest>
    {

        public CreateApplicantRequestValidator()
        {
            RuleFor(expression: x => x.Name)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(expression: x => x.FamilyName)
                .NotEmpty()
                .MinimumLength(5); 
            
            RuleFor(expression: x => x.Address)
                .NotEmpty()
                .MinimumLength(10);
            
            RuleFor(expression: x => x.EmailAddress)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Age).InclusiveBetween(18, 60);
        }
    }
}
