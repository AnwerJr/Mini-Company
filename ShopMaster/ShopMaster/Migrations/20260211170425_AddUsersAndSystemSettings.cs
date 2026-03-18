using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopMaster.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndSystemSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أجهزة إلكترونية ومعدات تقنية", true, "إلكترونيات", "Electronics" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ملابس رجالية ونسائية وأطفال", true, "ملابس", "Clothing" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "كتب ومراجع علمية وأدبية", true, "كتب", "Books" }
                });

            migrationBuilder.InsertData(
                table: "SystemSettings",
                columns: new[] { "Id", "Description", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, "اسم المتجر", "StoreName", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ShopMaster" },
                    { 2, "البريد الإلكتروني للمتجر", "StoreEmail", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@shopmaster.com" },
                    { 3, "رقم الهاتف", "StorePhone", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789" },
                    { 4, "عنوان المتجر", "StoreAddress", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "القاهرة، مصر" },
                    { 5, "رمز العملة", "Currency", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ج.م" },
                    { 6, "نسبة الضريبة المضافة (%)", "TaxRate", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "14" },
                    { 7, "تكلفة الشحن الافتراضية", "DefaultShippingCost", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "50" },
                    { 8, "الحد الأدنى لقيمة الطلب", "MinOrderAmount", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "100" },
                    { 9, "قيمة الطلب للشحن المجاني", "FreeShippingThreshold", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "500" },
                    { 10, "حد التنبيه لانخفاض المخزون", "LowStockAlert", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "10" },
                    { 11, "ساعات العمل", "WorkingHours", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "السبت - الخميس: 9 صباحاً - 9 مساءً" },
                    { 12, "بريد الدعم الفني", "SupportEmail", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "support@shopmaster.com" },
                    { 13, "مدة سياسة الإرجاع (بالأيام)", "ReturnPolicy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "14" },
                    { 14, "السماح بالشراء بدون تسجيل", "AllowGuestCheckout", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "true" },
                    { 15, "وضع الصيانة", "MaintenanceMode", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "false" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CreatedAt", "Email", "FullName", "IsActive", "LastLogin", "Password", "Phone", "Role", "Username" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@shopmaster.com", "المسؤول الرئيسي", true, null, "Admin@123", "0123456789", 1, "admin" },
                    { 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "manager@shopmaster.com", "مدير المبيعات", true, null, "Manager@123", "0123456780", 2, "manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_Status",
                table: "Shipments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TrackingNumber",
                table: "Shipments",
                column: "TrackingNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDate",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSettings_Key",
                table: "SystemSettings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_Status",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_TrackingNumber",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDate",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Status",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
