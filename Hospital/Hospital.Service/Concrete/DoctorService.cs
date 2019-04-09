using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Core.Helpers;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Service.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Visit> _visitRepository;
        private readonly ISpecializationService _specializationService;

        public DoctorService(IMapper mapper,
                             IRepository<Doctor> doctorRepository,
                             ISpecializationService specializationService,
                             IRepository<Visit> visitRepository)
        {
            _doctorRepository = doctorRepository;
            _visitRepository = visitRepository;
            _specializationService = specializationService;
            _mapper = mapper;
        }

        #region Public Methods
        public async Task<List<DoctorOutDTO>> GetAllActiveDoctors()
        {
            var result = await _doctorRepository.GetAllAsync<DoctorOutDTO>(x => new DoctorOutDTO
                                                                                {
                                                                                    UserID = x.Id,
                                                                                    FirstName = x.User.FirstName,
                                                                                    LastName = x.User.LastName
                                                                                });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specializationName">Specialization name</param>
        /// <param name="day">Day of visit</param>
        /// <param name="format">Format datetime</param>
        /// <returns>Null if doctors dont exist</returns>
        public async Task<List<DoctorAvailableVisitsOutDTO>> GetActiveDoctorsByDayAndSpecializationAsync(string specializationName, string day, string format)
        {
            List<DoctorAvailableVisitsOutDTO> result = null;

            if (specializationName == null || day == null || format == null)
            {
                return result;
            }

            var specializations = await _specializationService.GetByNameAsync(specializationName);
            if (specializations == null || specializations.Count == 0)
            {
                return result;
            }

            var specialization = specializations.First();

            DateTime visitDay;

            if (!DateTime.TryParseExact(day, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out visitDay))
            {
                return result;
            }

            // get all doctors for this specialization
            var doctors = await _doctorRepository.GetAllAsync<Doctor>(x => x,
                                                                      filter: x => x.SpecializationId == specialization.SpecializationId,
                                                                      includes: x => x.Include(y => y.Harmonogram).Include(y => y.Visits).Include(y => y.User));

            if (doctors.Count == 0)
            {
                return result;
            }

            var startDay = new DateTime(visitDay.Year, visitDay.Month, visitDay.Day, 0, 0, 0);
            var endDay = new DateTime(visitDay.Year, visitDay.Month, visitDay.Day, 23, 59, 59);


            result = new List<DoctorAvailableVisitsOutDTO>();
            doctors.ForEach(doctor =>
            {
                var doctorOutDTO = new DoctorAvailableVisitsOutDTO
                {
                    DoctorId = doctor.Id,
                    FirstName = doctor.User.FirstName,
                    LastName = doctor.User.LastName
                };

                var numbersOfArrangedVisits = doctor.Visits.Where(x => x.Date > startDay && x.Date < endDay)
                                                           .Select(x => x.NumberInDay)
                                                           .ToList();

                TimeSpan startWorkHour, endWorkHour;

                GetWorkHours(doctor.Harmonogram, visitDay, out startWorkHour, out endWorkHour);

                if (startWorkHour.CompareTo(TimeSpan.Zero) != 0 && endWorkHour.CompareTo(TimeSpan.Zero) != 0)
                {
                    doctorOutDTO.AvailableVisits = VisitDateTimeHelper.GetAvailableDateTimesInDay(visitDay,
                                                                                                  numbersOfArrangedVisits,
                                                                                                  startWorkHour,
                                                                                                  endWorkHour);
                    if (doctorOutDTO.AvailableVisits.Count > 0)
                    {
                        result.Add(doctorOutDTO);
                    }
                }
            });

            return result;
        }

        public async Task<Doctor> GetDoctorById(string DoctorID)
        {
            var result = await _doctorRepository.GetAsync<Doctor>(x => x, x => x.UserId == DoctorID);
            return result.FirstOrDefault();
        }

        public async Task<List<Visit>> GetAllDoctorVisitsByDoctorID(long DoctorID)
        {
            var result = await _visitRepository.GetAllAsync<Visit>(x => x, x => x.DoctorId == DoctorID);
            return result;
        }
  
        #endregion

        #region Private methods
        private void GetWorkHours(Harmonogram harmonogram, DateTime day, out TimeSpan startWorkHour, out TimeSpan endWorkHour)
        {
            var dayOfWeek = day.DayOfWeek;

            startWorkHour = TimeSpan.Zero;
            endWorkHour = TimeSpan.Zero;

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    startWorkHour = harmonogram.MondayStart;
                    endWorkHour = harmonogram.MondayEnd;
                    break;
                case DayOfWeek.Tuesday:
                    startWorkHour = harmonogram.TuesdayStart;
                    endWorkHour = harmonogram.TuesdayEnd;
                    break;
                case DayOfWeek.Wednesday:
                    startWorkHour = harmonogram.WednesdayStart;
                    endWorkHour = harmonogram.WednesdayEnd;
                    break;
                case DayOfWeek.Thursday:
                    startWorkHour = harmonogram.ThursdayStart;
                    endWorkHour = harmonogram.ThursdayEnd;
                    break;
                case DayOfWeek.Friday:
                    startWorkHour = harmonogram.FridayStart;
                    endWorkHour = harmonogram.FridayEnd;
                    break;
            }
        }
        #endregion
    }
}