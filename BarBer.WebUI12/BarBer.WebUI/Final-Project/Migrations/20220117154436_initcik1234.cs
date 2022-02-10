using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class initcik1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BlogPostComments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteByUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogPostComments_BlogPostComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BlogPostComments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPostComments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComments_BlogPostId",
                table: "BlogPostComments",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComments_ParentId",
                table: "BlogPostComments",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostComments");

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
    }
}
