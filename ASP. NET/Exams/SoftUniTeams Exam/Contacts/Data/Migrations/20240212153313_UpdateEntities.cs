using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Data.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersContacts_ApplicationUsers_ApplicationUserId",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersContacts_Contacts_ContactId",
                table: "ApplicationUsersContacts");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ApplicationUsersContacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersContacts_ApplicationUserId1",
                table: "ApplicationUsersContacts",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersContacts_ApplicationUsers_ApplicationUserId1",
                table: "ApplicationUsersContacts",
                column: "ApplicationUserId1",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersContacts_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersContacts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersContacts_Contacts_ContactId",
                table: "ApplicationUsersContacts",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersContacts_ApplicationUsers_ApplicationUserId1",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersContacts_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersContacts_Contacts_ContactId",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsersContacts_ApplicationUserId1",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ApplicationUsersContacts");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersContacts_ApplicationUsers_ApplicationUserId",
                table: "ApplicationUsersContacts",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersContacts_Contacts_ContactId",
                table: "ApplicationUsersContacts",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
