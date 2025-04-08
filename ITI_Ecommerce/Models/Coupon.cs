using System.ComponentModel.DataAnnotations;

namespace ITI_Ecommerce.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
        [Required]
        public int DisCountAmount { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int MaximumTimeUsed { get; set; }
        public List<OrderCoupon> OrderCoupons { get; set; }
    }
}
