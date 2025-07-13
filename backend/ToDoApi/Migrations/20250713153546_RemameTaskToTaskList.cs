using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class RemameTaskToTaskList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Tasks_TaskListId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TaskLists");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "TaskLists",
                newName: "IX_TaskLists_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_TaskLists_TaskListId",
                table: "TaskItems",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Users_UserId",
                table: "TaskLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_TaskLists_TaskListId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Users_UserId",
                table: "TaskLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists");

            migrationBuilder.RenameTable(
                name: "TaskLists",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLists_UserId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Tasks_TaskListId",
                table: "TaskItems",
                column: "TaskListId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
