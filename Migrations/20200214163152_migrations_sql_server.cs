using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Migrations
{
    public partial class migrations_sql_server : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Formulario = table.Column<string>(nullable: true),
                    Funcao = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Back = table.Column<string>(nullable: true),
                    Front = table.Column<string>(nullable: true),
                    Layout = table.Column<string>(nullable: true),
                    ObsApp = table.Column<string>(nullable: true),
                    OriginTable = table.Column<string>(nullable: true),
                    DestinTable = table.Column<string>(nullable: true),
                    ObsTable = table.Column<string>(nullable: true),
                    Projeto_Id = table.Column<int>(nullable: false),
                    MenuPai_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Linguagem = table.Column<string>(nullable: true),
                    Framework = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Ativo = table.Column<string>(nullable: false),
                    Perfil_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
