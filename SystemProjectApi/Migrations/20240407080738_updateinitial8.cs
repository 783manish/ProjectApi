using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class updateinitial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ProjectManagerId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ProjectManagerId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
