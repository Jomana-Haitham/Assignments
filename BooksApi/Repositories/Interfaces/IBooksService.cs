using BooksApi.Models.Entities;

namespace BooksApi.Services.Interfaces
{
    public interface IBooksService
    {
        List<Book> GetAll();

        Book? GetById(int id);
    }
}