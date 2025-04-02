using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.ViewModels
{
    public class OrderDetailModalViewModel
    {
        public string DivId { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
