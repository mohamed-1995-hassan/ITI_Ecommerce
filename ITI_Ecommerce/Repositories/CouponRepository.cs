using ITI_Ecommerce.Data;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Ecommerce.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _context;
        public CouponRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCoupon(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoupon(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Coupon>> GetCoupon()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon?> GetCouponById(int id)
        {
            return await _context.Coupons.FindAsync(id);
        }

        public async Task UpdateCoupon(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
        }
    }
}
