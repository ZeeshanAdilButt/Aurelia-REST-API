using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Service.Services.Implementation;
using Hahn.ApplicatonProcess.May2020.Service.Services.Interface;
using Hahn.ApplicatonProcess.May2020.Web.Contracts;
using Hahn.ApplicatonProcess.May2020.Web.Contracts.V1.Requests;
using Hahn.ApplicatonProcess.May2020.Web.Contracts.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApplicantsController : Controller
    {
        private readonly IApplicantService _applicantService;
        private readonly ICountryLookupService _countryLookupService;
        private readonly ILogger<ApplicantsController> _logger;

        public ApplicantsController(IApplicantService applicantService, ICountryLookupService countryLookupService, ILogger<ApplicantsController> logger)
        {
            this._applicantService = applicantService;
            this._countryLookupService = countryLookupService;
            this._logger = logger;
            _logger.LogInformation("Entering ApplicantsController constructor");
        }

        [HttpGet(ApiRoutes.Applicants.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _applicantService.GetApplicantsAsync());
        }

        [HttpGet(ApiRoutes.Applicants.Get)]
        public async Task<IActionResult> GetAsync( Guid ApplicantId)
        {

            var Applicant = await _applicantService.GetApplicantByIdAsync(ApplicantId);

            if (Applicant == null)
                return NotFound();

            return Ok(Applicant);
        }

        [HttpPost(ApiRoutes.Applicants.Create)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateApplicantRequest applicantRequest)
        {
            var isValid = await _countryLookupService.IsValidCountry(applicantRequest.CountryOfOrigin);

            if(!isValid)
                return BadRequest("Invalid country name");

            var isExists = await _applicantService.GetApplicantByEmailAsync(applicantRequest.EmailAddress);

            if (isExists)
                return BadRequest("User with this email already exists");

            //TODO: externlize conversions, using AutoMapper
            var applicant = new Applicant
            {
                Id = Guid.NewGuid(),
                Name = applicantRequest.Name,
                FamilyName = applicantRequest.FamilyName,
                Address = applicantRequest.Address,
                CountryOfOrigin = applicantRequest.CountryOfOrigin,
                EmailAddress = applicantRequest.EmailAddress,
                Age = applicantRequest.Age,
                Hired = applicantRequest.Hired
            };

            await _applicantService.CreateApplicantAsync(applicant);

            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUri + "/" + ApiRoutes.Applicants.Get.Replace("{ApplicantId}", applicant.Id.ToString());

            var response = new ApplicantResponse { Id = applicant.Id };

            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Applicants.Update)]
        public async Task<IActionResult> UpdateAsync(Guid ApplicantId, [FromBody]UpdateApplicantRequest request)
        {
            var applicant = await _applicantService.GetApplicantByIdAsync(ApplicantId);

            if (applicant?.EmailAddress == request.EmailAddress)
                return BadRequest("User with this email already exists");

            //TODO: externlize conversions, using AutoMapper
            applicant.Name = request.Name;
            applicant.FamilyName = request.FamilyName;
            applicant.Address = request.Address;
            applicant.CountryOfOrigin = request.CountryOfOrigin;
            applicant.EmailAddress = request.EmailAddress;
            applicant.Age = request.Age;
            applicant.Hired = request.Hired;

            var updated = await _applicantService.UpdateApplicantAsync(applicant);

            if (updated)
                return Ok(applicant.Id);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Applicants.Delete)]
        public async Task<IActionResult> DeleteAsync([FromBody]Guid applicantId)
        {
            var deleted = await _applicantService.DeleteApplicantAsync(applicantId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
