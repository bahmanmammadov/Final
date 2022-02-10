using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class Initciksd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_subscribes_CreatedByUserId",
                table: "subscribes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_subscribes_DeleteByUserId",
                table: "subscribes",
                column: "DeleteByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedByUserId",
                table: "Services",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DeleteByUserId",
                table: "Services",
                column: "DeleteByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallerys_CreatedByUserId",
                table: "Gallerys",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallerys_DeleteByUserId",
                table: "Gallerys",
                column: "DeleteByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CreatedByUserId",
                table: "Contacts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DeleteByUserId",
                table: "Contacts",
                column: "DeleteByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CreatedByUserId",
                table: "BlogPosts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_DeleteByUserId",
                table: "BlogPosts",
                column: "DeleteByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_User_CreatedByUserId",
                table: "BlogPosts",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_User_DeleteByUserId",
                table: "BlogPosts",
                column: "DeleteByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_User_CreatedByUserId",
                table: "Contacts",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_User_DeleteByUserId",
                table: "Contacts",
                column: "DeleteByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gallerys_User_CreatedByUserId",
                table: "Gallerys",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gallerys_User_DeleteByUserId",
                table: "Gallerys",
                column: "DeleteByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_User_CreatedByUserId",
                table: "Services",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_User_DeleteByUserId",
                table: "Services",
                column: "DeleteByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_subscribes_User_CreatedByUserId",
                table: "subscribes",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_subscribes_User_DeleteByUserId",
                table: "subscribes",
                column: "DeleteByUserId",
                principalSchema: "Membership",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_User_CreatedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_User_DeleteByUserId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_User_CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_User_DeleteByUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallerys_User_CreatedByUserId",
                table: "Gallerys");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallerys_User_DeleteByUserId",
                table: "Gallerys");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_User_CreatedByUserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_User_DeleteByUserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_subscribes_User_CreatedByUserId",
                table: "subscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_subscribes_User_DeleteByUserId",
                table: "subscribes");

            migrationBuilder.DropIndex(
                name: "IX_subscribes_CreatedByUserId",
                table: "subscribes");

            migrationBuilder.DropIndex(
                name: "IX_subscribes_DeleteByUserId",
                table: "subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Services_CreatedByUserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DeleteByUserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Gallerys_CreatedByUserId",
                table: "Gallerys");

            migrationBuilder.DropIndex(
                name: "IX_Gallerys_DeleteByUserId",
                table: "Gallerys");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_DeleteByUserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_CreatedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_DeleteByUserId",
                table: "BlogPosts");
        }
    }
}
