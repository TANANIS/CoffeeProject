using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    Password = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    ImgSrc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Language = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    UpdateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__A4AE64B824E3E012", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "ORDERDETAIL",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    OrderItem = table.Column<short>(type: "smallint", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Qty = table.Column<short>(type: "smallint", nullable: true),
                    Uom = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    UnitPrice = table.Column<short>(type: "smallint", nullable: true),
                    Totle = table.Column<int>(type: "int", nullable: true),
                    CreateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    UpdateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ORDERDET__C3905BCF9EC2CA22", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "ORDERHEADER",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Payment = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true),
                    OrderStatus = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    ShipStatus = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    CreateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    UpdateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ORDERHEA__C3905BCF70681B5F", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT",
                columns: table => new
                {
                    PayID = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentAmount = table.Column<int>(type: "int", nullable: true),
                    Currency = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    PaymentMethod = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    PaymentStatus = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    TransactionId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    CreateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    UpdateUser = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAYMENT__EE8FCE2F5B4376E1", x => x.PayID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<short>(type: "smallint", nullable: true),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Uom = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Baking = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Flavor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Dripbag = table.Column<bool>(type: "bit", nullable: true),
                    Timelimit = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fragrance = table.Column<byte>(type: "tinyint", nullable: true),
                    Sour = table.Column<byte>(type: "tinyint", nullable: true),
                    Bitter = table.Column<byte>(type: "tinyint", nullable: true),
                    Sweet = table.Column<byte>(type: "tinyint", nullable: true),
                    STRONG = table.Column<byte>(type: "tinyint", nullable: true),
                    Method = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ImgA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ImgB = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ImgC = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ImgD = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Weight = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__B40CC6EDED44FF8B", x => x.ProductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "ORDERDETAIL");

            migrationBuilder.DropTable(
                name: "ORDERHEADER");

            migrationBuilder.DropTable(
                name: "PAYMENT");

            migrationBuilder.DropTable(
                name: "PRODUCT");
        }
    }
}
