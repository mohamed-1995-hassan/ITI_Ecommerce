using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateCouponTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Coupon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Coupon",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
