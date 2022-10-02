using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spyglass.Server.Migrations
{
    public partial class AddGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetricGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metric_MetricGroupId",
                table: "Metric",
                column: "MetricGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_MetricGroup_MetricGroupId",
                table: "Metric",
                column: "MetricGroupId",
                principalTable: "MetricGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metric_MetricGroup_MetricGroupId",
                table: "Metric");

            migrationBuilder.DropTable(
                name: "MetricGroup");

            migrationBuilder.DropIndex(
                name: "IX_Metric_MetricGroupId",
                table: "Metric");
        }
    }
}
