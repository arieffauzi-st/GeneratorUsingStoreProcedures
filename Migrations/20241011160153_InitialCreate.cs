using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneratorUsingStoreProcedures.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblM_gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblM_gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblM_hobby",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblM_hobby", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblT_personal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    HobbyId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblT_personal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblT_personal_tblM_gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "tblM_gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblT_personal_tblM_hobby_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "tblM_hobby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblM_gender_Name",
                table: "tblM_gender",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblM_hobby_Name",
                table: "tblM_hobby",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblT_personal_GenderId",
                table: "tblT_personal",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblT_personal_HobbyId",
                table: "tblT_personal",
                column: "HobbyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblT_personal");

            migrationBuilder.DropTable(
                name: "tblM_gender");

            migrationBuilder.DropTable(
                name: "tblM_hobby");
        }
    }
}
