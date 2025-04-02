using System.ComponentModel.DataAnnotations;

namespace ITI_Ecommerce.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
