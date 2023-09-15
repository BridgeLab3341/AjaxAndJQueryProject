using Microsoft.EntityFrameworkCore.Migrations;

namespace AjaxAndJQueryProject.Migrations
{
    public partial class UserLoginFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserLogin_AjaxEmployee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AjaxEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_EmployeeId",
                table: "UserLogin",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
