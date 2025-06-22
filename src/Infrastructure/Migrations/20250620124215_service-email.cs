using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class serviceemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailGroups_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailProjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailProjects_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailLists_EmailGroups_EmailGroupId",
                        column: x => x.EmailGroupId,
                        principalTable: "EmailGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupSendingProjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailProjectId = table.Column<long>(type: "bigint", nullable: false),
                    EmailGroupId = table.Column<long>(type: "bigint", nullable: true),
                    AllGroups = table.Column<bool>(type: "bit", nullable: false),
                    Submitted = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSendingProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSendingProjects_EmailGroups_EmailGroupId",
                        column: x => x.EmailGroupId,
                        principalTable: "EmailGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupSendingProjects_EmailProjects_EmailProjectId",
                        column: x => x.EmailProjectId,
                        principalTable: "EmailProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailResponseStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailListId = table.Column<long>(type: "bigint", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailResponseStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailResponseStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailResponseStatuses_EmailLists_EmailListId",
                        column: x => x.EmailListId,
                        principalTable: "EmailLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailSendingStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailListId = table.Column<long>(type: "bigint", nullable: true),
                    EmailProjectId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SendingStatus = table.Column<int>(type: "int", nullable: false),
                    Retries = table.Column<int>(type: "int", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSendingStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailSendingStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailSendingStatuses_EmailLists_EmailListId",
                        column: x => x.EmailListId,
                        principalTable: "EmailLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailSendingStatuses_EmailProjects_EmailProjectId",
                        column: x => x.EmailProjectId,
                        principalTable: "EmailProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailGroups_AppUserId",
                table: "EmailGroups",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLists_AppUserId",
                table: "EmailLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLists_EmailGroupId",
                table: "EmailLists",
                column: "EmailGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailProjects_AppUserId",
                table: "EmailProjects",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailResponseStatuses_EmailListId",
                table: "EmailResponseStatuses",
                column: "EmailListId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailResponseStatuses_UserId",
                table: "EmailResponseStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendingStatuses_EmailListId",
                table: "EmailSendingStatuses",
                column: "EmailListId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendingStatuses_EmailProjectId",
                table: "EmailSendingStatuses",
                column: "EmailProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendingStatuses_UserId",
                table: "EmailSendingStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSendingProjects_EmailGroupId",
                table: "GroupSendingProjects",
                column: "EmailGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSendingProjects_EmailProjectId",
                table: "GroupSendingProjects",
                column: "EmailProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_UserId",
                table: "Servers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailResponseStatuses");

            migrationBuilder.DropTable(
                name: "EmailSendingStatuses");

            migrationBuilder.DropTable(
                name: "GroupSendingProjects");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "EmailLists");

            migrationBuilder.DropTable(
                name: "EmailProjects");

            migrationBuilder.DropTable(
                name: "EmailGroups");
        }
    }
}
