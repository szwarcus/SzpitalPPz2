using System.Threading.Tasks;

namespace Hospital.Repository.Abstract
{
    public interface IDbInitializer
    {
        Task Initialize(); 
    }
}