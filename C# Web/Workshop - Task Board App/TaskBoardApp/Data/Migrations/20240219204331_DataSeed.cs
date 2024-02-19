using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BoardId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ac790533-e3b3-4080-bef2-81d5708d7734", 0, "2edd4ad0-9a7a-4737-bd51-739434401bb8", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEFTOWZ9J9/feQoE8pMzAMtTeBgBYqQZmjwjRXBgk5xDBuNITzq8D+0wDOimuYN2aVg==", null, false, "8495b455-2de5-4dfc-bb12-f1479e44efc5", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 8, 3, 22, 43, 31, 636, DateTimeKind.Local).AddTicks(4123), "Implement better styling for all public pages", "ac790533-e3b3-4080-bef2-81d5708d7734", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 19, 22, 43, 31, 636, DateTimeKind.Local).AddTicks(4159), "Create Android client app for the TaskBoard RESTful API", "ac790533-e3b3-4080-bef2-81d5708d7734", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 19, 22, 43, 31, 636, DateTimeKind.Local).AddTicks(4163), "Create Windows Forms app for the TaskBoard RESTful API", "ac790533-e3b3-4080-bef2-81d5708d7734", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 19, 22, 43, 31, 636, DateTimeKind.Local).AddTicks(4165), "Implement [Create Task] page for adding new tasks", "ac790533-e3b3-4080-bef2-81d5708d7734", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac790533-e3b3-4080-bef2-81d5708d7734");
        }
    }
}
