using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardCostApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddClearingCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClearingCosts",
                table: "ClearingCosts");

            migrationBuilder.RenameTable(
                name: "ClearingCosts",
                newName: "clearing_costs");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "clearing_costs",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "clearing_costs",
                newName: "country_code");

            migrationBuilder.AlterColumn<int>(
                name: "cost",
                table: "clearing_costs",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clearing_costs",
                table: "clearing_costs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_clearing_costs",
                table: "clearing_costs");

            migrationBuilder.RenameTable(
                name: "clearing_costs",
                newName: "ClearingCosts");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "ClearingCosts",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "country_code",
                table: "ClearingCosts",
                newName: "Name");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "ClearingCosts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClearingCosts",
                table: "ClearingCosts",
                column: "Id");
        }
    }
}
