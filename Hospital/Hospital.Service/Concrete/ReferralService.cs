using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Concrete
{
    public class ReferralService : IReferralService
    {
        IRepository<Referral> _repositoryReferral;
        IRepository<Visit> _repositoryVisit;
        IRepository<Specialization> _repositorySpecialization;
        public ReferralService(IRepository<Referral> repositoryReferral, IRepository<Visit> repositoryVisit, IRepository<Specialization> repositorySpecialization)
        {
            _repositoryReferral = repositoryReferral;
            _repositoryVisit = repositoryVisit;
            _repositorySpecialization = repositorySpecialization;
        }
        public async Task Create(ReferralInDTO referral)
        {
            var visit =  await _repositoryVisit.GetAsync(x => x, x => x.Id == referral.VisitId);
            var specialization = await _repositorySpecialization.GetAsync(x => x, x => x.Id == referral.SpecializationId);
            var newReferral = new Referral() {
                Description = referral.Description,
                VisitId = referral.VisitId,
                Visit = visit.FirstOrDefault(),
                Specialization = specialization.FirstOrDefault(),
                SpecializationId = referral.SpecializationId,
                ExpiryDate = DateTime.Now.AddDays(30)                             
            };
            await _repositoryReferral.InsertAsync(newReferral);
        }
    }
}
