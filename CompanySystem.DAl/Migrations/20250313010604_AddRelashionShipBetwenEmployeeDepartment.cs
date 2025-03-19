using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySystem.DAl.Migrations
{
    public partial class AddRelashionShipBetwenEmployeeDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptID",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_DeptID",
                table: "employees",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DeptID",
                table: "employees",
                column: "DeptID",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DeptID",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_DeptID",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DeptID",
                table: "employees");
        }
    }
}
