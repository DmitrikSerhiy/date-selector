using Microsoft.EntityFrameworkCore.Migrations;

namespace DateSelector.Persistence.Migrations
{
    public partial class addSecondDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DateComparisonObjects");

            migrationBuilder.AddColumn<long>(
                name: "FirstDate",
                table: "DateComparisonObjects",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SecondDate",
                table: "DateComparisonObjects",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstDate",
                table: "DateComparisonObjects");

            migrationBuilder.DropColumn(
                name: "SecondDate",
                table: "DateComparisonObjects");

            migrationBuilder.AddColumn<long>(
                name: "Date",
                table: "DateComparisonObjects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
