using ITI_Ecommerce.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository _userRepository;
        public UserOrderController(IUserOrderRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userRepository.UserOrders();
            return View(orders);
        }
    }
}
