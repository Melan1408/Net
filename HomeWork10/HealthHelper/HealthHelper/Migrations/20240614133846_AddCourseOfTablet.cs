using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseOfTablet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseOfTablets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TabletId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfTablets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfTablets_FamilyMembers_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseOfTablets_Tablets_TabletId",
                        column: x => x.TabletId,
                        principalTable: "Tablets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfTablets_FamilyMemberId",
                table: "CourseOfTablets",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfTablets_TabletId",
                table: "CourseOfTablets",
                column: "TabletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseOfTablets");
        }
    }
}
