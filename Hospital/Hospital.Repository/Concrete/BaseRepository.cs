using System;
using System.Threading.Tasks;
using Hospital.Repository.Abstract;

namespace Hospital.Repository.Concrete
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository()
        {
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return false;
            }
        }
    }
}