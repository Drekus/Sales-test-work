namespace Sales.Models.DataModels
{
    public class OrderedBook
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
