using ITI_Ecommerce.Data;
using ITI_Ecommerce.Models;
using ITI_Ecommerce.ViewModels;

namespace ITI_Ecommerce.IRepositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByBookId(int bookId);
        Task ManageStock(StockDTO stockToManage);
        Task<IEnumerable<StockDisplayViewModel>> GetStocks(string sTerm = "");
    }
}
