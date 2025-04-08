using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.IRepositories
{
    public interface ICouponRepository
    {
        Task AddCoupon(Coupon coupon);
        Task UpdateCoupon(Coupon coupon);
        Task DeleteCoupon(Coupon coupon);
        Task<Coupon?> GetCouponById(int id);
        Task<IEnumerable<Coupon>> GetCoupon();
    }
}
