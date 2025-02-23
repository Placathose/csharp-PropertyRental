using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_PropertyRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLandlordAndPropertyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Leases_LeaseId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_LeaseId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "LeaseId",
                table: "Tenants");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_LandlordId",
                table: "Leases",
                column: "LandlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Landlords_LandlordId",
                table: "Leases",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "LandlordId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Landlords_LandlordId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_LandlordId",
                table: "Leases");

            migrationBuilder.AddColumn<int>(
                name: "LeaseId",
                table: "Tenants",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_LeaseId",
                table: "Tenants",
                column: "LeaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Leases_LeaseId",
                table: "Tenants",
                column: "LeaseId",
                principalTable: "Leases",
                principalColumn: "LeaseId");
        }
    }
}
