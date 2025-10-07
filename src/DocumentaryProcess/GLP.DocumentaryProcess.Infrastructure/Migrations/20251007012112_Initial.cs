using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GLP_DocumentaryProcess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "doc");

            migrationBuilder.CreateTable(
                name: "DocumentationTypes",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lodges",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberTypes",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_MemberTypes_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalSchema: "doc",
                        principalTable: "MemberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentations",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelativePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentations_DocumentationTypes_DocumentationTypeId",
                        column: x => x.DocumentationTypeId,
                        principalSchema: "doc",
                        principalTable: "DocumentationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentations_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "doc",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member_Lodges",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LodgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Lodges", x => x.Id);
                    table.UniqueConstraint("AK_Member_Lodges_MemberId_LodgeId", x => new { x.MemberId, x.LodgeId });
                    table.ForeignKey(
                        name: "FK_Member_Lodges_Lodges_LodgeId",
                        column: x => x.LodgeId,
                        principalSchema: "doc",
                        principalTable: "Lodges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_Lodges_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "doc",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_DocumentationTypeId",
                schema: "doc",
                table: "Documentations",
                column: "DocumentationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_MemberId",
                schema: "doc",
                table: "Documentations",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Lodges_LodgeId",
                schema: "doc",
                table: "Member_Lodges",
                column: "LodgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberTypeId",
                schema: "doc",
                table: "Members",
                column: "MemberTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentations",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "Member_Lodges",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "DocumentationTypes",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "Lodges",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "MemberTypes",
                schema: "doc");
        }
    }
}
