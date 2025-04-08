using System.ComponentModel.DataAnnotations;

namespace ITI_Ecommerce.Models
{
    public class OrderCoupon
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public Order Order { get; set; }
    }
}
