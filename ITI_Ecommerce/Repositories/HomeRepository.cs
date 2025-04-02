using ITI_Ecommerce.Data;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITI_Ecommerce.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeRepository(ApplicationDbContext db,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooks(string sTerm="", int categoryId = 0)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User!);
            

            var books = await _db.Books
                .Include(b => b.Category)
                .Include(b => b.Stock)
                .Include(w => w.WishLists.Where(wl => user == null || wl.UserId == user.Id))
                .Where(s =>string.IsNullOrEmpty(sTerm) || s.BookName!.ToLower().StartsWith(sTerm))
                .Select(b => new Book
            {
                Id = b.Id,
                Image = b.Image,
                AutherName = b.AutherName,
                BookName = b.BookName,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.CategoryName,
                Price = b.Price,
                Quantity = b.Stock == null ? 0 : b.Stock.Quantity,
                IsFavourite = b.WishLists.Any(),
            }).ToListAsync();
            if(categoryId > 0)
            {
                books = books.Where(b => b.CategoryId == categoryId).ToList();
            }
            return books;
        }
    }
}
