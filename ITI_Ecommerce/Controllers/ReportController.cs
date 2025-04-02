using ITI_Ecommerce.Constants;
using ITI_Ecommerce.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public IActionResult TopFiveSellingBooks(DateTime? sDate = null, DateTime? eDate = null)
        {
            try
            {
                DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
                DateTime endtDate = sDate ?? DateTime.UtcNow;
                var topSold = _reportRepository.GetTopUnitSold(startDate, endtDate);
                return View(topSold);
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "Something Went Wrong";
                return RedirectToAction("Index","Home");
            }
            
        }
    }
}
