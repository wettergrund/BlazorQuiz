using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorQuiz.Server.Data.Migrations
{
    public partial class added_dbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRefId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaModels_AspNetUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timer = table.Column<int>(type: "int", nullable: false),
                    UserRefId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_Quizzes_AspNetUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizRefId = table.Column<string>(type: "string", nullable: false),
                    MediaRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionModels_MediaModels_MediaRefId",
                        column: x => x.MediaRefId,
                        principalTable: "MediaModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionModels_Quizzes_QuizRefPublicId",
                        column: x => x.QuizRefId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserQuizModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    QuizRefPublicId = table.Column<string>(type: "string", nullable: false),
                    UserRefId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuizModels_AspNetUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserQuizModels_Quizzes_QuizRefPublicId",
                        column: x => x.QuizRefPublicId,
                        principalTable: "Quizzes",
                        principalColumn: "PublicId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaModels_UserRefId",
                table: "MediaModels",
                column: "UserRefId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_MediaRefId",
                table: "QuestionModels",
                column: "MediaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_QuizRefId",
                table: "QuestionModels",
                column: "QuizRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UserRefId",
                table: "Quizzes",
                column: "UserRefId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizModels_QuizRefId",
                table: "UserQuizModels",
                column: "QuizRefId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizModels_UserRefId",
                table: "UserQuizModels",
                column: "UserRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionModels");

            migrationBuilder.DropTable(
                name: "UserQuizModels");

            migrationBuilder.DropTable(
                name: "MediaModels");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
