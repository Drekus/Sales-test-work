using System;
using System.Collections.Generic;
using Sales.Models.DataModels;

namespace Sales.Models.ViewModels
{
    public class BooksPageViewModel : PageViewModel
    {
        public ICollection<Book> Books;

        public BooksPageViewModel(int pageNumber, int pageSize, int totalPages, ICollection<Book> books) : base(pageNumber, pageSize, totalPages)
        {
            Books = books;
        }
    }
}
