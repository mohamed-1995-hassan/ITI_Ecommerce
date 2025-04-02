using ITI_Ecommerce.Models;

namespace ITI_Ecommerce.ViewModels
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string STerm { get; set; }
        public int CategoryId { get; set; }
    }
}
