using Hahn.ApplicatonProcess.May2020.Data.Data;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Service.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Service.Services.Implementation
{
    public class ApplicantService : IApplicantService
    {
        private readonly DataContext _dataContext;
        public ApplicantService(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        /// <summary>
        /// CreateApplicantAsync addsan application to database
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        public async Task<bool> CreateApplicantAsync(Applicant applicant)
        {
            await _dataContext.Applicants.AddAsync(applicant);

            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        /// <summary>
        /// UpdateApplicantAsync updates the applicant in database
        /// </summary>
        /// <param name="applicantToUpdate"></param>
        /// <returns></returns>
        public async Task<bool> UpdateApplicantAsync(Applicant applicantToUpdate)
        {

            _dataContext.Applicants.Update(applicantToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        /// <summary>
        /// DeleteApplicantAsync deletes the applicant from database
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteApplicantAsync(Guid applicantId)
        {
            var applicant = await GetApplicantByIdAsync(applicantId);

            _dataContext.Applicants.Remove(applicant);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        /// <summary>
        /// GetApplicantByIdAsync gets the applicant by ID from database
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public async Task<Applicant> GetApplicantByIdAsync(Guid applicantId)
        {
            return await _dataContext.Applicants.SingleOrDefaultAsync(x => x.Id == applicantId);
        }

        public async Task<List<Applicant>> GetApplicantsAsync()
        {
            return await _dataContext.Applicants.ToListAsync();
        }

        public async Task<bool> GetApplicantByEmailAsync(string emailAddress)
        {
            var result = await _dataContext.Applicants.SingleOrDefaultAsync(x => x.EmailAddress == emailAddress);

            if (result == null)
                return false;

            return true;
        }
    }
}
