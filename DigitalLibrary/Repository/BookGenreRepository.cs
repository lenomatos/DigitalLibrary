using DigitalLibrary.Data;
using DigitalLibrary.Models;

namespace DigitalLibrary.Repository
{
    public class BookGenreRepository : Repository<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(DataContext context) : base(context) { }
    }
}
