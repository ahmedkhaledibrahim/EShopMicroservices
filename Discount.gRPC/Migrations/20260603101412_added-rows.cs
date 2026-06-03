using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.gRPC.Migrations
{
    /// <inheritdoc />
    public partial class addedrows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "ID", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 1, 150, "IPhone Discount", "IPhone X" },
                    { 2, 100, "Samsung Discount", "Samsung S10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
