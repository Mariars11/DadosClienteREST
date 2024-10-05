using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apsen.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    NOME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SOBRENOME = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    FLAG_STATUS_ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMAIL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ENDERECO_EMAIL = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMAIL_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ENDERECO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEP = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    ENDERECO = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NUMERO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BAIRRO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CIDADE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ESTADO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ENDERECO_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TELEFONE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DDD = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    NUMERO = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    TELEFONE_FIXO = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: true),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TELEFONE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TELEFONE_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_ID_CLIENTE",
                table: "EMAIL",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECO_ID_CLIENTE",
                table: "ENDERECO",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_TELEFONE_ID_CLIENTE",
                table: "TELEFONE",
                column: "ID_CLIENTE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMAIL");

            migrationBuilder.DropTable(
                name: "ENDERECO");

            migrationBuilder.DropTable(
                name: "TELEFONE");

            migrationBuilder.DropTable(
                name: "CLIENTE");
        }
    }
}
