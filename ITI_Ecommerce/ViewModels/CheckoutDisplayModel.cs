using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.ViewModels
{
    public class CheckoutDisplayModel
    {
        public CheckoutModel CheckoutModel { get; set; } = new();
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
