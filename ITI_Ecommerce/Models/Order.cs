﻿using System.ComponentModel.DataAnnotations;

namespace ITI_Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public int OrderStatusId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [MaxLength(30)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }
        public bool IsPaid { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
