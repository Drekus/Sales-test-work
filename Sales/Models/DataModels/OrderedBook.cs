using System;
using System.ComponentModel.DataAnnotations;

namespace Sales.Models.DataModels
{
    public class OrderedBook
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        [Display(Name = "Количество")]
        public int BookAmount { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Cost => Math.Round(Book.Price * BookAmount, 2);
    }
}
