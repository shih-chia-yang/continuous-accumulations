using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace marketplace.infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassifiedAds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId_Value = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    Title_Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text_Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Price_Currency_CurrencyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Price_Currency_InUse = table.Column<bool>(type: "bit", nullable: true),
                    Price_Currency_DecimalPlace = table.Column<decimal>(type: "decimal(2,0)", nullable: true),
                    Price_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedAds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size_Width = table.Column<int>(type: "int", nullable: true),
                    Size_Height = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ClassifiedAdId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_ClassifiedAds_ClassifiedAdId",
                        column: x => x.ClassifiedAdId,
                        principalTable: "ClassifiedAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ClassifiedAdId",
                table: "Pictures",
                column: "ClassifiedAdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "ClassifiedAds");
        }
    }
}
