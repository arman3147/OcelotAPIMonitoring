using Microsoft.EntityFrameworkCore.Migrations;

namespace OcelotAPIMonitoring.Migrations
{
    public partial class dropSourceIpColumnFromHttpLogContextTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceIP",
                table: "HttpContextLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceIP",
                table: "HttpContextLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
