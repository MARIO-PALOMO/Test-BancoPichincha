using Microsoft.EntityFrameworkCore.Migrations;

namespace banco_dao.Migrations
{
    public partial class CreacionRelacionesCuentayMovimiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuentaId",
                table: "Movimiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Cuenta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_CuentaId",
                table: "Movimiento",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_ClienteId",
                table: "Cuenta",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Cliente_ClienteId",
                table: "Cuenta",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_Cuenta_CuentaId",
                table: "Movimiento",
                column: "CuentaId",
                principalTable: "Cuenta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Cliente_ClienteId",
                table: "Cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_Cuenta_CuentaId",
                table: "Movimiento");

            migrationBuilder.DropIndex(
                name: "IX_Movimiento_CuentaId",
                table: "Movimiento");

            migrationBuilder.DropIndex(
                name: "IX_Cuenta_ClienteId",
                table: "Cuenta");

            migrationBuilder.DropColumn(
                name: "CuentaId",
                table: "Movimiento");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Cuenta");
        }
    }
}
