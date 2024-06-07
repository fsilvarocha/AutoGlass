using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoGlass.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Inativo = table.Column<bool>(type: "bit", nullable: false),
                    FabricadoEm = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdFornecedor = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: false),
                    DescricaoFornecedor = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    CNPJFornecedor = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
