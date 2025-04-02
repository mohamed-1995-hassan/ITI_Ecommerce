using ITI_Ecommerce.Data;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.ViewModels;

namespace ITI_Ecommerce.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<TopBookSoldViewModel> GetTopUnitSold(DateTime startDate, DateTime endDate)
        {
            
            var topBookSoldViewModels = (from o in _dbContext.Order
                     join od in _dbContext.OrderDetails on o.Id equals od.OrderId
                     join b in _dbContext.Books on od.BookId equals b.Id
                     where o.IsPaid && !o.IsDeleted && o.CreatedAt >= startDate && o.CreatedAt <= endDate
                     group od by new { b.BookName, b.AutherName, b.Id } into g
                     select new TopBookSoldViewModel
                     {                              
                         BookName = g.Key.BookName,                      
                         AutherName = g.Key.AutherName,                  
                         TotalPointOfSold = g.Sum(od => od.Quantity)    
                     }).OrderByDescending(s => s.TotalPointOfSold)
                       .Take(5)
                       .ToList();

            return topBookSoldViewModels;
        }
    }
}
