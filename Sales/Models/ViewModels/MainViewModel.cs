using System.Collections.Generic;
using Sales.Models.DataModels;

namespace Sales.Models.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string UserPromoCode;
        public Order UserOrder;
        public BooksPageViewModel BooksPageViewModel { get; set; }
       
    }
}
