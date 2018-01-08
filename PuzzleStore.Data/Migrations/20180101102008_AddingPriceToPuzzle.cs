using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PuzzleStore.Data.Migrations
{
    public partial class AddingPriceToPuzzle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Parts",
                table: "Puzzles",
                newName: "PartsCount");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Puzzles",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PuzzleId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    XCordinate = table.Column<int>(nullable: false),
                    YCordinate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Puzzles_PuzzleId",
                        column: x => x.PuzzleId,
                        principalTable: "Puzzles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PuzzleId",
                table: "Parts",
                column: "PuzzleId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_UserId",
                table: "Parts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Puzzles");

            migrationBuilder.RenameColumn(
                name: "PartsCount",
                table: "Puzzles",
                newName: "Parts");
        }
    }
}
