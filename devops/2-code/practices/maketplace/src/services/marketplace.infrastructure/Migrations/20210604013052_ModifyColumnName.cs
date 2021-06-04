using Microsoft.EntityFrameworkCore.Migrations;

namespace marketplace.infrastructure.Migrations
{
    public partial class ModifyColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_ClassifiedAds_ParentId",
                table: "Picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassifiedAds",
                table: "ClassifiedAds");

            migrationBuilder.RenameColumn(
                name: "FullName_Value",
                table: "UserProfiles",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "DisplayName_Value",
                table: "UserProfiles",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "Size_Width",
                table: "Picture",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Size_Height",
                table: "Picture",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "Title_Value",
                table: "ClassifiedAds",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Text_Value",
                table: "ClassifiedAds",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Price_Currency_InUse",
                table: "ClassifiedAds",
                newName: "InUse");

            migrationBuilder.RenameColumn(
                name: "Price_Currency_DecimalPlace",
                table: "ClassifiedAds",
                newName: "DecimalPlace");

            migrationBuilder.RenameColumn(
                name: "Price_Currency_CurrencyCode",
                table: "ClassifiedAds",
                newName: "CurrencyCode");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                table: "ClassifiedAds",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "OwnerId_Value",
                table: "ClassifiedAds",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "ClassifiedAdId1",
                table: "ClassifiedAds",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassifiedAds",
                table: "ClassifiedAds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_ClassifiedAds_ParentId",
                table: "Picture",
                column: "ParentId",
                principalTable: "ClassifiedAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_ClassifiedAds_ParentId",
                table: "Picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassifiedAds",
                table: "ClassifiedAds");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "UserProfiles",
                newName: "FullName_Value");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "UserProfiles",
                newName: "DisplayName_Value");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Picture",
                newName: "Size_Width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Picture",
                newName: "Size_Height");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ClassifiedAds",
                newName: "Title_Value");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "ClassifiedAds",
                newName: "Text_Value");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "ClassifiedAds",
                newName: "OwnerId_Value");

            migrationBuilder.RenameColumn(
                name: "InUse",
                table: "ClassifiedAds",
                newName: "Price_Currency_InUse");

            migrationBuilder.RenameColumn(
                name: "DecimalPlace",
                table: "ClassifiedAds",
                newName: "Price_Currency_DecimalPlace");

            migrationBuilder.RenameColumn(
                name: "CurrencyCode",
                table: "ClassifiedAds",
                newName: "Price_Currency_CurrencyCode");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ClassifiedAds",
                newName: "Price_Amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClassifiedAds",
                newName: "ClassifiedAdId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassifiedAds",
                table: "ClassifiedAds",
                column: "ClassifiedAdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_ClassifiedAds_ParentId",
                table: "Picture",
                column: "ParentId",
                principalTable: "ClassifiedAds",
                principalColumn: "ClassifiedAdId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
