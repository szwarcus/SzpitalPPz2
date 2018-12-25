using System.Threading.Tasks;
using Hospital.Service.InDTOs;

namespace Hospital.Service.Abstract
{
    public interface IVisitService
    {
        Task<bool> ArrangeVisit(ArrangeVisitInDTO model);
    }
}