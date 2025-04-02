using ITI_Ecommerce.Data;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ITI_Ecommerce.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public WishListRepository(ApplicationDbContext db,
                                  IHttpContextAccessor httpContextAccessor,
                                  UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Book>> GetWishLists()
        {
            var user = await _userManager
                .GetUserAsync(_httpContextAccessor?.HttpContext?.User!);
            var wishLists = _db.WishLists
                               .Include(b => b.Book)
                               .Where(w => w.UserId == user!.Id)
                               .ToList();
            return wishLists.Select(w => w.Book);
        }

        public async Task<bool> ManageWishList(int bookId)
        {
            var user = await _userManager
                            .GetUserAsync(_httpContextAccessor?.HttpContext?.User!);

            if (user is null)
                throw new UnauthorizedAccessException("user must logged in");

            var book = _db.Books.Find(bookId);

            if (book is null)
                throw new InvalidOperationException("No Book With This Id in The System");

            var wishList = _db
                .WishLists
                .FirstOrDefault(w => w.BookId == bookId && w.UserId == user.Id);

            if (wishList is not null)
                _db.WishLists.Remove(wishList);

            else
            {
                var wishListItem = new WishList
                {
                    BookId = bookId,
                    UserId = user.Id,
                };
                _db.WishLists.Add(wishListItem);
            }
            _db.SaveChanges();
            var status = _db
                .WishLists
                .Any(w => w.BookId == bookId && w.UserId == user.Id);
            return status;
        }

        public async Task<int> GetWishListItemCount(string userId = "")
        {
            var id = "";
            if (string.IsNullOrEmpty(userId))
            {
                var user = _httpContextAccessor.HttpContext?.User!;
                id = _userManager.GetUserId(user);
            }
            var items = await _db.WishLists
                          .Where(c => c.UserId == id)
                          .ToListAsync();

            return items.Count();
        }
    }
}
