using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySystem.DAl.Migrations
{
    public partial class UpdateImageEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "employees",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "employees",
                newName: "Image");
        }
    }
}
