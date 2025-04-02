using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.IRepositories
{
    public interface IBookRepository
    {
        Task AddBook(Book book);
        Task DeleteBook(Book book);
        Task<Book?> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooks();
        Task UpdateBook(Book book);
    }
}
