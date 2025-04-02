using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_paid_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Order");
        }
    }
}
