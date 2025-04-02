using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using ITI_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITI_Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(string sterm = "", int categoryId = 0)
        {
            var books = await _homeRepository.GetBooks(sterm, categoryId);
            var categories = await _homeRepository.Categories();
            var bookModel = new BookDisplayModel
            {
                Categories = categories,
                Books = books,
                CategoryId = categoryId,
                STerm = sterm
            };
            return View(bookModel);
        }
    }
}
