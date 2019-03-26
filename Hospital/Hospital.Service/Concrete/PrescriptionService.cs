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
    public class PrescriptionService : IPrescriptionService
    {
        IRepository<Visit> _repositoryVisit;
        IRepository<Prescription> _repositoryPrescription;
        public PrescriptionService(IRepository<Visit> repository, IRepository<Prescription> repositoryPrescription)
        {
            _repositoryVisit = repository;
            _repositoryPrescription = repositoryPrescription;
        }

        public async Task Create(PrescriptionInDTO prescriptionInDTO)
        {
            var visits = await _repositoryVisit.GetAsync(x => x ,x => x.Id == prescriptionInDTO.VisitId);
            var visit = visits.FirstOrDefault();

            Prescription prescription = new Prescription()
            {
                Comments = prescriptionInDTO.Comments,
                DueDate = prescriptionInDTO.DueDate,
                VisitId = prescriptionInDTO.VisitId,
                Visit = visit,
                PrescriptionMedicaments = new List<PrescriptionMedicament>()
                
            };

            foreach (var item in prescriptionInDTO.Medicaments)
            {
                PrescriptionMedicament prescriptionMedicament = new PrescriptionMedicament();
                prescriptionMedicament.Medicament = item;
                prescriptionMedicament.Prescription = prescription;

                prescription.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

             await _repositoryPrescription.InsertAsync(prescription);
          
        }
    }
}
