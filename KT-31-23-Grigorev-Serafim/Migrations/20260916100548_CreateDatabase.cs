using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KT_31_23_Grigorev_Serafim.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    c_discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название дисциплины"),
                    c_discipline_is_deleted = table.Column<bool>(type: "bool", nullable: false, comment: "Признак удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.c_discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_specialty",
                columns: table => new
                {
                    c_specialty_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор специальности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_specialty_title = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название специальности"),
                    c_specialty_code = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "Код специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_specialty_specialty_id", x => x.c_specialty_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    c_group_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор группы")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_group_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название группы"),
                    c_group_course = table.Column<int>(type: "int4", nullable: false, comment: "Номер курса"),
                    c_group_is_deleted = table.Column<bool>(type: "bool", nullable: false, comment: "Признак удаления"),
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.c_group_id);
                    table.ForeignKey(
                        name: "fk_f_specialty_id",
                        column: x => x.SpecialtyId,
                        principalTable: "cd_specialty",
                        principalColumn: "c_specialty_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    c_student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_student_firstname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    c_student_is_deleted = table.Column<bool>(type: "bool", nullable: false, comment: "Признак удаления"),
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.c_student_id);
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.GroupId,
                        principalTable: "cd_group",
                        principalColumn: "c_group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    c_grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор оценки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_grade_value = table.Column<int>(type: "int4", nullable: false, comment: "Значение оценки"),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.c_grade_id);
                    table.ForeignKey(
                        name: "fk_f_discipline_id",
                        column: x => x.DisciplineId,
                        principalTable: "cd_discipline",
                        principalColumn: "c_discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_f_student_id",
                        column: x => x.StudentId,
                        principalTable: "cd_student",
                        principalColumn: "c_student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_f_discipline_id",
                table: "cd_grade",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_f_student_id",
                table: "cd_grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_fk_f_specialty_id",
                table: "cd_group",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_fk_f_group_id",
                table: "cd_student",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");

            migrationBuilder.DropTable(
                name: "cd_specialty");
        }
    }
}
