using ITI_Ecommerce.Constants;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<IActionResult> Index(string sTerm="")
        {
            var stocks = await _stockRepository.GetStocks(sTerm);
            return View(stocks);
        }

        public async Task<IActionResult> ManageStock(int bookId)
        {
            var existingStock =  await _stockRepository.GetStockByBookId(bookId);
            var stockViewModel = new StockDTO
            {
                BookId = bookId,
                Quantity = existingStock != null ? existingStock.Quantity : 0
            };
            return View(stockViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ManageStock(StockDTO stockDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(stockDTO);
            }
            try
            {
                await _stockRepository.ManageStock(stockDTO);
                TempData["successMessage"] = "Stock Updated Successfully";
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "something Went Wrong";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
