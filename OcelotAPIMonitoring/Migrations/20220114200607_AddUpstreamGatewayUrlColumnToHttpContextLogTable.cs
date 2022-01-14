using Microsoft.EntityFrameworkCore.Migrations;

namespace OcelotAPIMonitoring.Migrations
{
    public partial class AddUpstreamGatewayUrlColumnToHttpContextLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpstreamGatewayURL",
                table: "HttpContextLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpstreamGatewayURL",
                table: "HttpContextLogs");
        }
    }
}
