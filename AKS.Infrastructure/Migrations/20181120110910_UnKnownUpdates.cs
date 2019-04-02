using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AKS.Infrastructure.Migrations
{
    public partial class UnKnownUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicFragment",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    ParentTopicId = table.Column<Guid>(nullable: false),
                    ChildTopicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicFragment", x => new { x.ProjectId, x.ParentTopicId, x.ChildTopicId });
                    table.ForeignKey(
                        name: "FK_TopicFragment_Topic_ChildTopicId",
                        column: x => x.ChildTopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicFragment_Topic_ParentTopicId",
                        column: x => x.ParentTopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicFragment_ChildTopicId",
                table: "TopicFragment",
                column: "ChildTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicFragment_ParentTopicId",
                table: "TopicFragment",
                column: "ParentTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicFragment");
        }
    }
}
