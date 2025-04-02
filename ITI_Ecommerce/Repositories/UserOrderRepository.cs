using ITI_Ecommerce.Data;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using ITI_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITI_Ecommerce.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public UserOrderRepository(ApplicationDbContext db,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _db.Order.FindAsync(data.OrderId);
            if (order == null)
                throw new InvalidOperationException("order not found in the system");

            order.OrderStatusId = data.OrderId;
            await _db.SaveChangesAsync();

        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _db.Order.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
            return await _db.OrderStatus.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _db.Order.FindAsync(orderId);
            if (order == null)
                throw new InvalidOperationException("order not found in the system");

            order.IsPaid = !order.IsPaid;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders = _db.Order
               .Include(o => o.OrderStatus)
               .Include(o => o.OrderDetails)
               .ThenInclude(o => o.Book)
               .ThenInclude(o => o.Category)
               .AsQueryable();

            if(!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("user must logged in");
                }
                orders = orders.Where(o => o.UserId == userId);
            }
            return await orders.ToListAsync();
        }

        private string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User!;
            var userId = _userManager.GetUserId(user);
            return userId;
        }
    }
}
