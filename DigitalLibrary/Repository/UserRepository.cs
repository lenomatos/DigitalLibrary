using DigitalLibrary.Data;
using DigitalLibrary.Models;

namespace DigitalLibrary.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }
    }
}
