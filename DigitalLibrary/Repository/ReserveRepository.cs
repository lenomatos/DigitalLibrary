using DigitalLibrary.Data;
using DigitalLibrary.Models;

namespace DigitalLibrary.Repository
{
    public class ReserveRepository : Repository<Reserve>, IReserveRepository
    {
        public ReserveRepository(DataContext context) : base(context) { }
    }
}
