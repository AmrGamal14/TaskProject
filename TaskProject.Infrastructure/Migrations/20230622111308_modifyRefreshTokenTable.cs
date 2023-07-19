using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserRefreshToken",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserRefreshToken");
        }
    }
}
