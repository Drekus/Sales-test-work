using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sales.Models.DataModels
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("PromoCode")]
        public string PromoCodeValue { get; set; }
        public PromoCode PromoCode { get; set; }
        public bool IsСheckouted { get; set; }
        public ICollection<OrderedBook> OrderedBooks { get; set; } = new List<OrderedBook>();

        [Display(Name = "Итоговая стоимость")]
        public decimal TotalCost => Math.Round(OrderedBooks.Sum(b=>b.Cost), 2);

        public bool IsValid => TotalCost >= 2000;
    }
}
