using DigitalLibrary.Data;
using DigitalLibrary.Models;

namespace DigitalLibrary.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context) { }
    }
}
