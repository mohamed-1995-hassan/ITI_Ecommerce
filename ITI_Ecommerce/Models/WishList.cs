namespace ITI_Ecommerce.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
