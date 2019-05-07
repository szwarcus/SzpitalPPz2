using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Model.Entities;
using Hospital.Service.InDTOs;
using Hospital.Service.InDTOs.Shared;
using Hospital.Service.OutDTOs;

    public interface IVisitService
    {
        Task<bool> ArrangeVisit(ArrangeVisitInDTO model);
        Task UpdateVisit(UpdateVisitInDTO model);
        Task<Visit> GetById(GetByIdInDTO model);
        Task<PastAndNextVisitsOutDTO> GetBaseInfoVisitsInPastAndNextDaysAsync(string userId, int days = 30);
        Task<List<VisitOutDTO>> GetCompletedVisits(long userId);
        Task<VisitDetailsOutDTO> GetVisitDetails(long visitId, DateTime date);
    }
