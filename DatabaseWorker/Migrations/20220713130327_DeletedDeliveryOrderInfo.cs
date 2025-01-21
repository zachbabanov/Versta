using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseWorker.Migrations
{
    public partial class DeletedDeliveryOrderInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DeliveryOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DeletedDeliveryOrderInfo",
                columns: table => new
                {
                    DeliveryOrderId = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedDeliveryOrderInfo", x => x.DeliveryOrderId);
                    table.ForeignKey(
                        name: "FK_DeletedDeliveryOrderInfo_DeliveryOrder_DeliveryOrderId",
                        column: x => x.DeliveryOrderId,
                        principalTable: "DeliveryOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedDeliveryOrderInfo");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DeliveryOrder");
        }
    }
}
