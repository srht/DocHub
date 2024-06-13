using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class documentsoftags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Documents_DDocumentId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_DDocumentId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DDocumentId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "DDocumentTag",
                columns: table => new
                {
                    DocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDocumentTag", x => new { x.DocumentsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DDocumentTag_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DDocumentTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DDocumentTag_TagsId",
                table: "DDocumentTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DDocumentTag");

            migrationBuilder.AddColumn<Guid>(
                name: "DDocumentId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DDocumentId",
                table: "Tags",
                column: "DDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Documents_DDocumentId",
                table: "Tags",
                column: "DDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
