using BooksApi.Models.Entities;
using BooksApi.Services.Interfaces;

namespace BooksApi.Services
{
    public class BooksService : IBooksService
    {
        private readonly List<Book> _books =
        [
            new Book
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Year = 2008
            },

            new Book
            {
                Id = 2,
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Year = 1999
            },

            new Book
            {
                Id = 3,
                Title = "Design Patterns",
                Author = "Erich Gamma",
                Year = 1994
            }
        ];

        public List<Book> GetAll()
        {
            return _books;
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
    }
}