using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;

namespace Hospital.Service.Concrete
{
    public class VisitService : IVisitService
    {
        private IMapper _mapper;

        private IRepository<Visit> _visitRepository;
        private IRepository<Patient> _patientRepository;
        private IRepository<Doctor> _doctorRepository;
        private IRepository<Nurse> _nurseRepository;

        public VisitService(IMapper mapper,
                            IRepository<Visit> visitRepository,
                            IRepository<Patient> patientRepository,
                            IRepository<Doctor> doctorRepository,
                            IRepository<Nurse> nurseRepository)
        {
            _visitRepository = visitRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _nurseRepository = nurseRepository;
            _mapper = mapper;
        }

        //public Task<bool> ArrangeVisit(ArrangeVisitInDTO model)
        //{
        //    if (model == null) return null;


        //}
    }
}
