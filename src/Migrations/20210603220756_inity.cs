using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Migrations
{
    public partial class inity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formulario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Back = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Front = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObsTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Projeto_Id = table.Column<int>(type: "int", nullable: false),
                    MenuPai_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linguagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Framework = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Perfil_Id = table.Column<int>(type: "int", nullable: false)
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
