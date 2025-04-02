using ITI_Ecommerce.ViewModels;

namespace ITI_Ecommerce.IRepositories
{
    public interface IReportRepository
    {
        List<TopBookSoldViewModel> GetTopUnitSold(DateTime startDate, DateTime endDate);
    }
}
