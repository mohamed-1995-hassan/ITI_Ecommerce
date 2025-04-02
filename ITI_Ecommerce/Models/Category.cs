using System.ComponentModel.DataAnnotations;

namespace ITI_Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
        public List<Book> Books { get; set; }

    }
}
