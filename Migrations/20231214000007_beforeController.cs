using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Elibrary.Migrations
{
    public partial class beforeController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_starSubject_AspNetUsers_UserId",
                table: "starSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_starSubject_subject_SubId",
                table: "starSubject");

            migrationBuilder.DropTable(
                name: "q_a");

            migrationBuilder.DropPrimaryKey(
                name: "PK_starSubject",
                table: "starSubject");

            migrationBuilder.RenameTable(
                name: "starSubject",
                newName: "studyingSubject");

            migrationBuilder.RenameColumn(
                name: "isStar",
                table: "studyingSubject",
                newName: "isFavorite");

            migrationBuilder.RenameIndex(
                name: "IX_starSubject_UserId",
                table: "studyingSubject",
                newName: "IX_studyingSubject_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_starSubject_SubId",
                table: "studyingSubject",
                newName: "IX_studyingSubject_SubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_studyingSubject",
                table: "studyingSubject",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_question_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_question_subject_SubId",
                        column: x => x.SubId,
                        principalTable: "subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topic_subject_SubId",
                        column: x => x.SubId,
                        principalTable: "subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "classSub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classSub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_classSub_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classSub_subject_SubId",
                        column: x => x.SubId,
                        principalTable: "subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_answer_question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answer_QuestionId",
                table: "answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_classSub_ClassId",
                table: "classSub",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_classSub_SubId",
                table: "classSub",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_question_SubId",
                table: "question",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_question_UserId",
                table: "question",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_topic_SubId",
                table: "topic",
                column: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_studyingSubject_AspNetUsers_UserId",
                table: "studyingSubject",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_studyingSubject_subject_SubId",
                table: "studyingSubject",
                column: "SubId",
                principalTable: "subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studyingSubject_AspNetUsers_UserId",
                table: "studyingSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_studyingSubject_subject_SubId",
                table: "studyingSubject");

            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "classSub");

            migrationBuilder.DropTable(
                name: "topic");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studyingSubject",
                table: "studyingSubject");

            migrationBuilder.RenameTable(
                name: "studyingSubject",
                newName: "starSubject");

            migrationBuilder.RenameColumn(
                name: "isFavorite",
                table: "starSubject",
                newName: "isStar");

            migrationBuilder.RenameIndex(
                name: "IX_studyingSubject_UserId",
                table: "starSubject",
                newName: "IX_starSubject_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_studyingSubject_SubId",
                table: "starSubject",
                newName: "IX_starSubject_SubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_starSubject",
                table: "starSubject",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "q_a",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_q_a", x => x.Id);
                    table.ForeignKey(
                        name: "FK_q_a_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_q_a_subject_SubId",
                        column: x => x.SubId,
                        principalTable: "subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_q_a_SubId",
                table: "q_a",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_q_a_UserId",
                table: "q_a",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_starSubject_AspNetUsers_UserId",
                table: "starSubject",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_starSubject_subject_SubId",
                table: "starSubject",
                column: "SubId",
                principalTable: "subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
