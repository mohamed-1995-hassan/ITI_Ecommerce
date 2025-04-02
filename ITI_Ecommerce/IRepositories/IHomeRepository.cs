using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.IRepositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> GetBooks(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Category>> Categories();
    }
}
