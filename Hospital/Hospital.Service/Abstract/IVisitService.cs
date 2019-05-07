using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Model.Entities;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IVisitService
    {
        Task<bool> ArrangeVisit(ArrangeVisitInDTO model);
        Task UpdateVisit(UpdateVisitInDTO model);
        Task<Visit> GetById(long Id);
        Task<PastAndNextVisitsOutDTO> GetBaseInfoVisitsInPastAndNextDaysAsync(string userId);
        Task<List<VisitOutDTO>> GetCompletedVisits(long userId);
        Task<VisitDetailsOutDTO> GetVisitDetails(long visitId, DateTime date);
    }
}