using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sales.Data;
using Sales.Models.DataModels;
using Sales.Models.ViewModels;

namespace Sales.Services
{
    public class AuthenticationService
    {
        private ApplicationDbContext _db;

        public AuthenticationService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BaseViewModel> LoginWithGeneratedPromoCodeAsync(HttpContext httpContext)
        {
            string promoCode;
            bool isExist;
            do
            {
                promoCode = GeneretePromoCode();
                isExist = await IsPromoCodeExistAsync(promoCode);
            } while (isExist);
            _db.PromoCodes.Add(new PromoCode {Value = promoCode});
            await _db.SaveChangesAsync();
            return await LoginAsync(httpContext, promoCode);
        }

        public async Task<BaseViewModel> LoginAsync(HttpContext httpContext, string promoCode)
        {
            BaseViewModel model = new BaseViewModel();
            bool isExist = await IsPromoCodeExistAsync(promoCode);
            if (isExist)
                httpContext.Response.Cookies.Append("SalesPromoCode", promoCode);
            else
                model.ErrorMessage = "Попытка входа не удалась";
            return model;
        }

        public void Logout(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.ContainsKey("SalesPromoCode"))
                httpContext.Response.Cookies.Delete("SalesPromoCode");
        }

        public async Task<bool> IsPromoCodeExistAsync(string promoCode) 
            => await _db.PromoCodes.AnyAsync(pc => pc.Value.Contains(promoCode) && pc.Value.Length == promoCode.Length);

        private static string GeneretePromoCode()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int promoLenght = 12;
            var random = new Random();
            var sb = new StringBuilder(promoLenght);
            while (sb.Length < promoLenght)
            {
                var randomIndex = random.Next(0, alphabet.Length - 1);
                var randomLetter = alphabet[randomIndex];
                sb.Append(randomLetter);
            }
            var promoCode = sb.ToString();
            return promoCode;
        }
    }
}
