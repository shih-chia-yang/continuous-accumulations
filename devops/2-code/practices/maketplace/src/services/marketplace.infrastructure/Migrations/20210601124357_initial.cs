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
                    ClassifiedAdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifiedAdId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ClassifiedAds", x => x.ClassifiedAdId);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size_Width = table.Column<int>(type: "int", nullable: true),
                    Size_Height = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_Pictures_ClassifiedAds_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ParentId",
                table: "Pictures",
                column: "ParentId");
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
