using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sales.Data;
using Sales.Models.DataModels;
using Sales.Models.ViewModels;

namespace Sales.Services
{
    public class SalesService
    {
        private ApplicationDbContext _db;
        private AuthenticationService _authService;
        private const int _pageSize = 2;

        public SalesService(ApplicationDbContext db, AuthenticationService authService)
        {
            _db = db;
            _authService = authService;
        }

        public async Task<MainViewModel> GetModel(HttpContext httpContext, int page)
        {
            MainViewModel model = new MainViewModel{BooksPageViewModel = await GetBooksPage(page) };
            if (!httpContext.Request.Cookies.ContainsKey("SalesPromoCode"))
                return model;
            string promoCode = httpContext.Request.Cookies["SalesPromoCode"];
            bool isExist = await _authService.IsPromoCodeExistAsync(promoCode);
            if (isExist)
                model.UserOrder = await GetUserOrder(promoCode);
            else
                model.ErrorMessage = "Ваш промокод недействителен! Удалите куки.";
            return model;
        }

        public async Task<MainViewModel> AddToOrder(HttpContext httpContext, int page, int bookId)
        {
            MainViewModel model = await GetModelForUser(httpContext, page);
            if (model.HaveError)
                return model;

            Book book = await _db.Books.FindAsync(bookId);
            if (book == null)
            {
                model.ErrorMessage = "Добавляемая книга не найдена.";
                return model;
            }

            if (book.Quantity < 1)
            {
                model.ErrorMessage = $"Извините, данная книга закончилась.";
                return model;
            }

            if (model.UserOrder == null)
            {
                model.UserOrder = new Order {PromoCodeValue = model.UserPromoCode };
                model.UserOrder.OrderedBooks = new List<OrderedBook>
                {
                    new OrderedBook
                    {
                        Order = model.UserOrder,
                        Book = book
                    }
                };

                _db.Orders.Add(model.UserOrder);
                await _db.SaveChangesAsync();
                return model;
            }

            if (model.UserOrder.IsСheckouted)
            {
                model.ErrorMessage = "Заказ уже оформлен. Изменить его невозможно.";
                return model;
            }
           
            OrderedBook orderedBook = model.UserOrder.OrderedBooks.FirstOrDefault(ob => ob.BookId == bookId);
            if (orderedBook != null)
            {
                model.ErrorMessage = "Вы уже заказали данную книгу.";
                return model;               
            }

            orderedBook = new OrderedBook
            {
                Order = model.UserOrder,
                Book = book
            };
            model.UserOrder.OrderedBooks.Add(orderedBook);

            _db.Orders.Update(model.UserOrder);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<MainViewModel> RemoveFromOrder(HttpContext httpContext, int page, int bookId)
        {
            MainViewModel model = await GetModelForUser(httpContext, page);
            if (model.HaveError)
                return model;

            if (model.UserOrder == null)
            {
                model.ErrorMessage = "Заказ не не найден.";
                return model;
            }

            if (model.UserOrder.IsСheckouted)
            {
                model.ErrorMessage = "Заказ уже оформлен. Изменить его невозможно.";
                return model;
            }

            OrderedBook orderedBook = model.UserOrder.OrderedBooks.FirstOrDefault(ob => ob.BookId == bookId);
            if (orderedBook == null)
            {
                model.ErrorMessage = "Удаляемая книга в заказе не найдена";
                return model;
            }

            model.UserOrder.OrderedBooks.Remove(orderedBook);          
            _db.Orders.Update(model.UserOrder);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<MainViewModel> Сheckout(HttpContext httpContext, int page)
        {
            MainViewModel model = await GetModelForUser(httpContext, page);
            if (model.HaveError)
                return model;

            if (model.UserOrder == null)
            {
                model.ErrorMessage = "Заказ не не найден.";
                return model;
            }

            if (!model.UserOrder.IsValid)
            {
                model.ErrorMessage = "Нельзя оформить заказ менее, чем на 2000 рублей.";
                return model;
            }

            if (model.UserOrder.IsСheckouted)
            {
                model.ErrorMessage = "Заказ уже оформлен.";
                return model;
            }

            model.UserOrder.IsСheckouted = true;
            foreach (var ob in model.UserOrder.OrderedBooks)
            {
                if (ob.Book.Quantity < 1)
                {
                    model.ErrorMessage = $"Извините. Книга '{ob.Book.Title}' закончилась на складе. Она была удалена из вашего заказа.";
                    model.UserOrder.OrderedBooks.Remove(ob);
                    break;
                }
                ob.Book.Quantity -= 1;
            }
            _db.Orders.Update(model.UserOrder);
            await _db.SaveChangesAsync();

            model = await GetModelForUser(httpContext, page);
            return model;
        }

        private async Task<MainViewModel> GetModelForUser(HttpContext httpContext, int page)
        {
            MainViewModel model = new MainViewModel { BooksPageViewModel = await GetBooksPage(page) };

            bool isAuthorezied = httpContext.Request.Cookies.ContainsKey("SalesPromoCode");
            if (!isAuthorezied)
            {
                model.ErrorMessage = "Вы не авторизованы. Ввойдите снова.";
                return model;
            }

            string promoCode = httpContext.Request.Cookies["SalesPromoCode"];
            bool isExist = !string.IsNullOrWhiteSpace(promoCode) && await _authService.IsPromoCodeExistAsync(promoCode);
            if (!isExist)
            {
                model.ErrorMessage = "Ваш промокод недействителен! Удалите куки.";
                return model;
            }

            model.UserOrder = await GetUserOrder(promoCode);
            model.UserPromoCode = promoCode;
            return model;
        }

        private async Task<BooksPageViewModel> GetBooksPage(int page)
        {
            IQueryable<Book> source = _db.Books.Where(b => b.Quantity > 0);
            int count = await source.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)_pageSize);
            if (page < 1)
                page = 1;
            else if (page > totalPages)
                page = totalPages;
            var books = await source.Skip((page - 1) * _pageSize).Take(_pageSize).ToListAsync();
            BooksPageViewModel pageViewModel = new BooksPageViewModel(page, _pageSize, totalPages, books);
            return pageViewModel;
        }

        private async Task<Order> GetUserOrder(string promoCode) => await _db.Orders
            .Include(o=>o.OrderedBooks)
            .ThenInclude(ob=>ob.Book)
            .FirstOrDefaultAsync(o => o.PromoCodeValue.Contains(promoCode) 
                                 && o.PromoCodeValue.Length == promoCode.Length);
    }
}
