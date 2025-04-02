using ITI_Ecommerce.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListRepository _wishListRepository;
        public WishListController(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }
        public async Task<IActionResult> GetUserWishList()
        {
            var wishList = (await _wishListRepository.GetWishLists()).ToList();
            return View(wishList);
        }
        public async Task<IActionResult> ManageWishList(int bookId)
        {
            var status = await _wishListRepository.ManageWishList(bookId);
            return Ok(status);
        }

        public async Task<IActionResult> GetWishListItemCount(int bookId)
        {
            var count = await _wishListRepository.GetWishListItemCount();
            return Ok(count);
        }
    }
}
