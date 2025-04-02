using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.IRepositories
{
    public interface IWishListRepository
    {
        Task<IEnumerable<Book>> GetWishLists();
        Task<bool> ManageWishList(int bookId);
        Task<int> GetWishListItemCount(string userId = "");
    }
}
