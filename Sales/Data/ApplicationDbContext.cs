using Microsoft.EntityFrameworkCore;
using Sales.Models.DataModels;

namespace Sales.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderedBook>()
                .HasKey(t => new { t.OrderId, t.BookId });

            modelBuilder.Entity<OrderedBook>()
                .HasOne(ob => ob.Order)
                .WithMany(o => o.OrderedBooks)
                .HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<OrderedBook>()
                .HasOne(ob => ob.Book)
                .WithMany(b => b.OrderedBooks)
                .HasForeignKey(ob => ob.BookId);

            #region BooksSeed

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#",
                    Author = "Рихтер Джеффри",
                    ISBN = "978-5-496-00433-6",
                    PublishingYear = 2013,
                    Price = 999.99m,
                    Quantity = 5
                },
                new Book
                {
                    Id = 2,
                    Title = "Искусство автономного тестирования с примерами на C#",
                    Author = "Ошероув Рой",
                    ISBN = "978-5-94074-945-5",
                    PublishingYear = 2014,
                    Price = 699.99m,
                    Quantity = 6
                },
                new Book
                {
                    Id = 3,
                    Title = "Архитектура компьютера",
                    Author = "Таненбаум Э., Остин Т.",
                    ISBN = "978-5-496-00337-7",
                    PublishingYear = 2013,
                    Price = 850m,
                    Quantity = 4
                },
                new Book
                {
                    Id =4,
                    Title = "Современные операционные системы",
                    Author = "Таненбаум Эндрю",
                    ISBN = "978-5-4461-1155-8",
                    PublishingYear = 2010,
                    Price = 850m,
                    Quantity = 2
                },
                new Book
                {
                    Id = 5,
                    Title = "Объектно-ориентированное мышление",
                    Author = "Вайсфельд Мэтт",
                    ISBN = " 978-5-496-00793-1",
                    PublishingYear = 2014,
                    Price = 630m,
                    Quantity = 3
                },
                new Book
                {
                    Id = 6,
                    Title = "Внутреннее устройство Windows",
                    Author = " Соломон Дэвид, Руссинович Марк, Ионеску Алекс , Йосифович Павел",
                    ISBN = "978-5-4461-0663-9",
                    PublishingYear = 2018,
                    Price = 1800,
                    Quantity = 16
                },
                new Book
                {
                    Id = 7,
                    Title = "Алгоритмы: построение и анализ",
                    Author = "Томас Х. Кормен, Чарльз И. Лейзерсон, Рональд Л. Ривест, Клиффорд Штайн ",
                    ISBN = "978-5-8459-2016-4",
                    PublishingYear = 2015,
                    Price = 1950,
                    Quantity = 4
                },
                new Book
                {
                    Id = 8,
                    Title = "Программирование: принципы и практика использования C++",
                    Author = "Страуструп Бьярне",
                    ISBN = "978-5-8459-1705-8",
                    PublishingYear = 2011,
                    Price = 750m,
                    Quantity = 2
                },
                new Book
                {
                    Id = 9,
                    Title = "Структуры данных и алгоритмы в Java",
                    Author = "Лафоре Р.",
                    ISBN = " 978-5-496-00740-5",
                    PublishingYear = 2011,
                    Price = 880m,
                    Quantity = 3
                },
                new Book
                {
                    Id = 10,
                    Title = "С# 6.0. Справочник. Полное описание языка",
                    Author = "Албахарв Джозеф, Албахари Бен",
                    ISBN = "978-5-8459-2087-4",
                    PublishingYear = 2016,
                    Price = 1150m,
                    Quantity = 4
                },
                new Book
                {
                    Id = 11,
                    Title = "Программирование искусственного интеллекта в приложениях",
                    Author = "Джонс М. Т.",
                    ISBN = "978-5940747468",
                    PublishingYear = 2011,
                    Price = 750m,
                    Quantity = 2
                },
                new Book
                {
                    Id = 12,
                    Title = "СУБД для программиста. Базы данных изнутри. ",
                    Author = "Тарасов С. В. ",
                    ISBN = "978-2-7466-7383-0 ",
                    PublishingYear = 2015,
                    Price = 630m,
                    Quantity = 1
                },
                new Book
                {
                    Id = 13,
                    Title = "Просто о больших данных",
                    Author = "Джудит Гурвиц, Алан Ньюджент Ферн Халпер, Марсия Кауфман",
                    ISBN = "978-5-699-85806-4",
                    PublishingYear = 2015,
                    Price = 1450m,
                    Quantity = 4
                },
                new Book
                {
                    Id = 14,
                    Title = "Машинное обучение. Наука и искусств.о построения алгоритмов, которые извлекают знания из данных",
                    Author = "Флах Петер",
                    ISBN = "978-5-97060-273-7",
                    PublishingYear = 2012,
                    Price = 840m,
                    Quantity = 2
                },
                new Book
                {
                    Id = 15,
                    Title = "Алгоритмические трюки для программистов",
                    Author = "Уоррен Генри",
                    ISBN = "978-5-8459-0572-9 ",
                    PublishingYear = 2004,
                    Price = 610m,
                    Quantity = 3
                },
                new Book
                {
                    Id = 16,
                    Title = "Язык программирования C++",
                    Author = "Липпман Стенли Б., Лажойе Жози, Му Барбара Э.",
                    ISBN = "978-5-8459-1839-0",
                    PublishingYear = 2016,
                    Price = 1180m,
                    Quantity = 8
                },
                new Book
                {
                    Id = 17,
                    Title = "Алгоритмы. Теория и практическое применение",
                    Author = "Стивенс Род",
                    ISBN = "978-5-699-81729-0",
                    PublishingYear = 2016,
                    Price = 1350m,
                    Quantity = 5
                }
            );

            #endregion
        }

        public DbSet<Sales.Models.DataModels.OrderedBook> OrderedBook { get; set; }
    }
}
