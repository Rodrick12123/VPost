using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class AddedBlogReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReplyToId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ReplyToId",
                table: "BlogPosts");
        }
    }
}
