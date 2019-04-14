using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodDeliveryServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingradients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Image = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingradients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Image = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaIngradients",
                columns: table => new
                {
                    PizzaId = table.Column<Guid>(nullable: false),
                    IngradientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaIngradients", x => new { x.PizzaId, x.IngradientId });
                    table.ForeignKey(
                        name: "FK_PizzaIngradients_Ingradients_IngradientId",
                        column: x => x.IngradientId,
                        principalTable: "Ingradients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaIngradients_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngradients_IngradientId",
                table: "PizzaIngradients",
                column: "IngradientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaIngradients");

            migrationBuilder.DropTable(
                name: "Ingradients");

            migrationBuilder.DropTable(
                name: "Pizza");
        }
    }
}
