using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaleNumber",
                table: "Sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "'V-' || EXTRACT(YEAR FROM CURRENT_DATE)::TEXT || TO_CHAR(CURRENT_DATE, 'MM') || '-' || LEFT(gen_random_uuid()::TEXT, 8)",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "'V-' || EXTRACT(YEAR FROM CURRENT_DATE)::TEXT || '-' || LEFT(gen_random_uuid()::TEXT, 8)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaleNumber",
                table: "Sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "'V-' || EXTRACT(YEAR FROM CURRENT_DATE)::TEXT || '-' || LEFT(gen_random_uuid()::TEXT, 8)",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "'V-' || EXTRACT(YEAR FROM CURRENT_DATE)::TEXT || TO_CHAR(CURRENT_DATE, 'MM') || '-' || LEFT(gen_random_uuid()::TEXT, 8)");
        }
    }
}
