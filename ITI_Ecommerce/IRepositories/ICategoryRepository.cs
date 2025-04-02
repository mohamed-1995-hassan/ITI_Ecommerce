using ITI_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Ecommerce.IRepositories
{
    public interface ICategoryRepository
    {
        Task AddGenre(Category category);
        Task UpdateGenre(Category category);

        Task DeleteGenre(Category category);

        Task<Category?> GetGenreById(int id);

        Task<IEnumerable<Category>> GetGenres();
    }
}
