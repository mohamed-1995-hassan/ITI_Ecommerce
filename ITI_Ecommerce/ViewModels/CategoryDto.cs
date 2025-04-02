using System.ComponentModel.DataAnnotations;

namespace ITI_Ecommerce.ViewModels
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
    }
}
