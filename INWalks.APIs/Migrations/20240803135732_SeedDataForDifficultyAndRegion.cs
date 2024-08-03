using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace INWalks.APIs.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForDifficultyAndRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("07e117da-4af0-4d67-a726-f1ef77c8bd30"), "Medium" },
                    { new Guid("137bfca4-40da-4d74-8439-0cb65d8c4a14"), "Hard" },
                    { new Guid("d91666cb-a11d-4b02-9021-30acbb4420d3"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2d398afc-dc76-4da9-893f-dd84b243ff44"), "KLR", "Kerala region", null },
                    { new Guid("4ede635c-0b25-4f6d-8893-2e4e44570517"), "BOM", "Bombay region", null },
                    { new Guid("ac9d1931-9f78-4a6e-9247-9067b2d96d2f"), "CHN", "Chennai region", null },
                    { new Guid("bf5b41fa-2415-488c-943d-a69d1df0ff7e"), "PUN", "Pune region", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("07e117da-4af0-4d67-a726-f1ef77c8bd30"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("137bfca4-40da-4d74-8439-0cb65d8c4a14"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d91666cb-a11d-4b02-9021-30acbb4420d3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2d398afc-dc76-4da9-893f-dd84b243ff44"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4ede635c-0b25-4f6d-8893-2e4e44570517"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ac9d1931-9f78-4a6e-9247-9067b2d96d2f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bf5b41fa-2415-488c-943d-a69d1df0ff7e"));
        }
    }
}
