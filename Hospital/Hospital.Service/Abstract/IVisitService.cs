namespace Hospital.Service.Abstract
{
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
    }
}