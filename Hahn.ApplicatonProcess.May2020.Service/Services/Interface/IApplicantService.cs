using Hahn.ApplicatonProcess.May2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Service.Services.Interface
{
    public interface IApplicantService
    {
        Task<bool> CreateApplicantAsync(Applicant applicant);
        Task<bool> DeleteApplicantAsync(Guid applicantId);
        Task<Applicant> GetApplicantByIdAsync(Guid applicantId);
        Task<bool> UpdateApplicantAsync(Applicant applicantToUpdate);
        Task<List<Applicant>> GetApplicantsAsync();
        Task<bool> GetApplicantByEmailAsync(string emailAddress);
    }
}