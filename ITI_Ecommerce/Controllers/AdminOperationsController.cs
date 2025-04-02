using ITI_Ecommerce.Constants;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using ITI_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_Ecommerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminOperationsController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepository;
        public AdminOperationsController(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _userOrderRepository.UserOrders(true);
            return View(orders);
        }
        public async Task<IActionResult> TogglePaymentStatus(int orderId)
        {
            try
            {
                await _userOrderRepository.TogglePaymentStatus(orderId);
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction(nameof(AllOrders));
        }

        public async Task<IActionResult> UpdatePaymentStatus(int orderId)
        {
            var order = await _userOrderRepository.GetOrderById(orderId);

            if (order == null)
                throw new InvalidOperationException("order not found in the system");

            var orderStatusesList = (await _userOrderRepository
                                .GetOrderStatuses())
                                .Select(status => new SelectListItem
                                {
                                    Text = status.StatusName,
                                    Value = status.Id.ToString(),
                                    Selected = order.OrderStatusId == status.Id
                                });
            var data = new UpdateOrderStatusModel
            {
                OrderStatusId = order.OrderStatusId,
                OrderStatusList = orderStatusesList,
                OrderId = orderId
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatus(UpdateOrderStatusModel data)
        {
            if (!ModelState.IsValid)
            {
                var orderStatusesList = (await _userOrderRepository
                                .GetOrderStatuses())
                                .Select(status => new SelectListItem
                                {
                                    Text = status.StatusName,
                                    Value = status.Id.ToString(),
                                    Selected = data.OrderStatusId == status.Id
                                });
                return View(orderStatusesList);
            }
            try
            {
                await _userOrderRepository.ChangeOrderStatus(data);
                TempData["msg"] = "Updated Successfully";
            }
            catch (Exception ex) 
            {
                TempData["msg"] = "SomeThing Went Wrong";
            }
            return RedirectToAction(nameof(UpdatePaymentStatus), new { orderId = data.OrderId });
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
