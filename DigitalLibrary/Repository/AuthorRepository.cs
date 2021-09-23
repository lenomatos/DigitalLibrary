using DigitalLibrary.Data;
using DigitalLibrary.Models;

namespace DigitalLibrary.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context) { }

    }
}
