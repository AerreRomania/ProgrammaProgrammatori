using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PP.EntityFramework.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    SectorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.SectorID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    OnlineStatus = table.Column<bool>(nullable: false),
                    JobType = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Angajati",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodAngajat = table.Column<string>(nullable: true),
                    Angajat = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    DataNasterii = table.Column<DateTime>(nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    DataAngajarii = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: false),
                    Linie = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: true),
                    IdSector = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angajati_Sector_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sector",
                        principalColumn: "SectorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Articol = table.Column<string>(nullable: true),
                    Descriere = table.Column<string>(nullable: true),
                    CostProductie = table.Column<double>(nullable: true),
                    IdStagiune = table.Column<int>(nullable: true),
                    Finete = table.Column<string>(nullable: true),
                    Prezzo = table.Column<double>(nullable: true),
                    SectorID = table.Column<int>(nullable: true),
                    IdSector = table.Column<int>(nullable: false),
                    Centes = table.Column<double>(nullable: true),
                    Stagione = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articole_Sector_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sector",
                        principalColumn: "SectorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammerTask",
                columns: table => new
                {
                    ProgrammerTaskID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartTask = table.Column<DateTime>(nullable: false),
                    EndTask = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TaskCompleted = table.Column<bool>(nullable: false),
                    ProgrammerID = table.Column<int>(nullable: false),
                    ArticleID = table.Column<int>(nullable: false),
                    JobTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammerTask", x => x.ProgrammerTaskID);
                    table.ForeignKey(
                        name: "FK_ProgrammerTask_Articole_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Angajati_SectorID",
                table: "Angajati",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Articole_SectorID",
                table: "Articole",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammerTask_ArticleID",
                table: "ProgrammerTask",
                column: "ArticleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Angajati");

            migrationBuilder.DropTable(
                name: "ProgrammerTask");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Articole");

            migrationBuilder.DropTable(
                name: "Sector");
        }
    }
}
