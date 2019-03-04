using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Models.DataModels
{
    public class Book
    {
        public int Id { get; set; }
        [Required]      
        public string ISBN { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Display(Name = "Год публикации")]
        public int PublishingYear { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Осталось книг")]
        public int Quantity { get; set; }

        public ICollection<OrderedBook> OrderedBooks { get; set; } = new List<OrderedBook>();


    }
}
