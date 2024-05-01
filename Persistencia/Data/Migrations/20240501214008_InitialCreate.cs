using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    IdCandidato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCompleto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Genero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CiudadRecidencia = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescripcionHabilidades = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Experiencia = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlLinkedIn = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => x.IdCandidato);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    IdRefreshToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.IdRefreshToken);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Candidato",
                columns: new[] { "IdCandidato", "CiudadRecidencia", "DescripcionHabilidades", "Email", "Experiencia", "FechaNacimiento", "Genero", "NombreCompleto", "Telefono", "UrlLinkedIn" },
                values: new object[,]
                {
                    { 1, "Bogotá", "Desarrollo de software", "juan@example.com", "5 años", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Juan Pérez", "123456789", "https://www.linkedin.com/in/juanperez" },
                    { 2, "Medellín", "Gestión de proyectos", "maria@example.com", "8 años", new DateTime(1985, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "María López", "987654321", "https://www.linkedin.com/in/marialopez" },
                    { 3, "Cali", "Diseño gráfico", "alex@example.com", "3 años", new DateTime(1988, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Otro", "Alex García", "456789123", "https://www.linkedin.com/in/alexgarcia" },
                    { 4, "Barranquilla", "Marketing digital", "ana@example.com", "6 años", new DateTime(1992, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Ana Torres", "741852963", "https://www.linkedin.com/in/anatorres" },
                    { 5, "Cartagena", "Desarrollo web", "david@example.com", "7 años", new DateTime(1987, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "David Martínez", "369258147", "https://www.linkedin.com/in/davidmartinez" },
                    { 6, "Bucaramanga", "Diseño UX/UI", "laura@example.com", "4 años", new DateTime(1993, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Laura Ramírez", "654987321", "https://www.linkedin.com/in/lauraramirez" },
                    { 7, "Santa Marta", "Administración de bases de datos", "diego@example.com", "9 años", new DateTime(1984, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Diego Sánchez", "852369741", "https://www.linkedin.com/in/diegosanchez" },
                    { 8, "Cúcuta", "Desarrollo móvil", "carolina@example.com", "6 años", new DateTime(1991, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Carolina Martín", "987123654", "https://www.linkedin.com/in/carolinamartin" },
                    { 9, "Pereira", "Gestión de proyectos", "javier@example.com", "7 años", new DateTime(1989, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Javier Fernández", "369741852", "https://www.linkedin.com/in/javierfernandez" },
                    { 10, "Manizales", "Desarrollo de aplicaciones móviles", "valentina@example.com", "3 años", new DateTime(1995, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Valentina Ruiz", "258147369", "https://www.linkedin.com/in/valentinaruiz" },
                    { 11, "Bogotá", "Análisis de datos", "gabriel@example.com", "5 años", new DateTime(1987, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Gabriel Gómez", "147258369", "https://www.linkedin.com/in/gabrielgomez" },
                    { 12, "Medellín", "Marketing digital", "paula@example.com", "4 años", new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Paula Herrera", "123789456", "https://www.linkedin.com/in/paulaherrera" },
                    { 13, "Cali", "Desarrollo web", "alejandro@example.com", "8 años", new DateTime(1986, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Alejandro Torres", "987654321", "https://www.linkedin.com/in/alejandrotorres" },
                    { 14, "Barranquilla", "Diseño gráfico", "natalia@example.com", "3 años", new DateTime(1994, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Natalia Díaz", "369852147", "https://www.linkedin.com/in/nataliadiaz" },
                    { 15, "Cartagena", "Administración de sistemas", "andres@example.com", "10 años", new DateTime(1983, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Andrés López", "741258963", "https://www.linkedin.com/in/andreslopez" },
                    { 16, "Bucaramanga", "Desarrollo de software", "sofia@example.com", "6 años", new DateTime(1993, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Sofía García", "852369741", "https://www.linkedin.com/in/sofiagarcia" },
                    { 17, "Santa Marta", "Gestión de proyectos", "carlos@example.com", "7 años", new DateTime(1988, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Carlos Hernández", "123456789", "https://www.linkedin.com/in/carloshernandez" },
                    { 18, "Cúcuta", "Diseño UX/UI", "isabella@example.com", "5 años", new DateTime(1990, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Isabella Martínez", "987654321", "https://www.linkedin.com/in/isabellamartinez" },
                    { 19, "Pereira", "Desarrollo móvil", "miguel@example.com", "8 años", new DateTime(1985, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Miguel Rodríguez", "369258147", "https://www.linkedin.com/in/miguelrodriguez" },
                    { 20, "Manizales", "Marketing digital", "valeria@example.com", "6 años", new DateTime(1992, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Valeria López", "741852963", "https://www.linkedin.com/in/valerialopez" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Gerente" },
                    { 3, "Empleado" },
                    { 4, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Email", "Password", "UserName" },
                values: new object[] { 1, "britodelgado514@gmail.com", "123456", "Sicer Brito" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UsuarioId",
                table: "RefreshToken",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UserName",
                table: "Usuario",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RolId",
                table: "UsuarioRoles",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
