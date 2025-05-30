using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricalBillingRecommendation.Migrations
{
    /// <inheritdoc />
    public partial class AbleNullableThresholdInPricingTier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "threshold",
                table: "pricing_tiers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "threshold",
                table: "pricing_tiers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
