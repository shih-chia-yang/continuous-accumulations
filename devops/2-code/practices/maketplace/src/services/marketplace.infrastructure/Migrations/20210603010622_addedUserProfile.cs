using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace marketplace.infrastructure.Migrations
{
    public partial class addedUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.CreateTable(
                name: "Picture",
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
                    table.PrimaryKey("PK_Picture", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_Picture_ClassifiedAds_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName_Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisplayName_Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ParentId",
                table: "Picture",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size_Height = table.Column<int>(type: "int", nullable: true),
                    Size_Width = table.Column<int>(type: "int", nullable: true)
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
    }
}
