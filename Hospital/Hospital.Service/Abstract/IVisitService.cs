using System.Threading.Tasks;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IVisitService
    {
        Task<bool> ArrangeVisit(ArrangeVisitInDTO model);
        Task UpdateVisit(UpdateVisitInDTO model);
        Task<PastAndNextVisitsOutDTO> GetBaseInfoVisitsInPastAndNext30DaysAsync(string userId);
    }
}