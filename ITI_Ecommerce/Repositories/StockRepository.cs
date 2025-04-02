using ITI_Ecommerce.Data;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using ITI_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ITI_Ecommerce.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StockRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Stock?> GetStockByBookId(int bookId) =>
             await _applicationDbContext.Stocks.FirstOrDefaultAsync(s => s.BookId == bookId);


        public async Task ManageStock(StockDTO stockToManage)
        {
            var existingStock = await GetStockByBookId(stockToManage.BookId);
            if (existingStock is null)
            {
                var stock = new Stock { BookId = stockToManage.BookId, Quantity = stockToManage.Quantity };
                _applicationDbContext.Stocks.Add(stock);
            }
            else
                existingStock.Quantity = stockToManage.Quantity;

            await _applicationDbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<StockDisplayViewModel>> GetStocks(string sTerm = "")
        {


            var stockDisplayViewModels = await _applicationDbContext.Books
                                 .Include(b => b.Stock)
                                 .Where(book => string.IsNullOrWhiteSpace(sTerm) ||
                                        book.BookName!.ToLower().Contains(sTerm.ToLower()))
                                 .Select(book => new StockDisplayViewModel
                                 {
                                     BookId = book.Id,
                                     BookName = book.BookName,
                                     Quantity = book.Stock == null ? 0 : book.Stock.Quantity
                                 }).ToListAsync();
            return stockDisplayViewModels;
        }
    }
}




