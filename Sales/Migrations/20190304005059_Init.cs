using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ISBN = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    PublishingYear = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PromoCodeValue = table.Column<string>(nullable: false),
                    IsСheckouted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_PromoCodes_PromoCodeValue",
                        column: x => x.PromoCodeValue,
                        principalTable: "PromoCodes",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedBook",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    BookAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedBook", x => new { x.OrderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_OrderedBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedBook_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "Price", "PublishingYear", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, "Рихтер Джеффри", "978-5-496-00433-6", 999.99m, 2013, 5, "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#" },
                    { 15, "Уоррен Генри", "978-5-8459-0572-9 ", 610m, 2004, 3, "Алгоритмические трюки для программистов" },
                    { 14, "Флах Петер", "978-5-97060-273-7", 840m, 2012, 2, "Машинное обучение. Наука и искусств.о построения алгоритмов, которые извлекают знания из данных" },
                    { 13, "Джудит Гурвиц, Алан Ньюджент Ферн Халпер, Марсия Кауфман", "978-5-699-85806-4", 1450m, 2015, 4, "Просто о больших данных" },
                    { 12, "Тарасов С. В. ", "978-2-7466-7383-0 ", 630m, 2015, 1, "СУБД для программиста. Базы данных изнутри. " },
                    { 11, "Джонс М. Т.", "978-5940747468", 750m, 2011, 2, "Программирование искусственного интеллекта в приложениях" },
                    { 10, "Албахарв Джозеф, Албахари Бен", "978-5-8459-2087-4", 1150m, 2016, 4, "С# 6.0. Справочник. Полное описание языка" },
                    { 16, "Липпман Стенли Б., Лажойе Жози, Му Барбара Э.", "978-5-8459-1839-0", 1180m, 2016, 8, "Язык программирования C++" },
                    { 9, "Лафоре Р.", " 978-5-496-00740-5", 880m, 2011, 3, "Структуры данных и алгоритмы в Java" },
                    { 7, "Томас Х. Кормен, Чарльз И. Лейзерсон, Рональд Л. Ривест, Клиффорд Штайн ", "978-5-8459-2016-4", 1950m, 2015, 4, "Алгоритмы: построение и анализ" },
                    { 6, " Соломон Дэвид, Руссинович Марк, Ионеску Алекс , Йосифович Павел", "978-5-4461-0663-9", 1800m, 2018, 16, "Внутреннее устройство Windows" },
                    { 5, "Вайсфельд Мэтт", " 978-5-496-00793-1", 630m, 2014, 3, "Объектно-ориентированное мышление" },
                    { 4, "Таненбаум Эндрю", "978-5-4461-1155-8", 850m, 2010, 2, "Современные операционные системы" },
                    { 3, "Таненбаум Э., Остин Т.", "978-5-496-00337-7", 850m, 2013, 4, "Архитектура компьютера" },
                    { 2, "Ошероув Рой", "978-5-94074-945-5", 699.99m, 2014, 6, "Искусство автономного тестирования с примерами на C#" },
                    { 8, "Страуструп Бьярне", "978-5-8459-1705-8", 750m, 2011, 2, "Программирование: принципы и практика использования C++" },
                    { 17, "Стивенс Род", "978-5-699-81729-0", 1350m, 2016, 5, "Алгоритмы. Теория и практическое применение" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedBook_BookId",
                table: "OrderedBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromoCodeValue",
                table: "Orders",
                column: "PromoCodeValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedBook");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PromoCodes");
        }
    }
}
