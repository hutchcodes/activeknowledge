using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AKS.Infrastructure.Migrations
{
    public partial class RelatedTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedTopic",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    ParentTopicId = table.Column<Guid>(nullable: false),
                    ChildTopicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedTopic", x => new { x.ProjectId, x.ParentTopicId, x.ChildTopicId });
                    table.ForeignKey(
                        name: "FK_RelatedTopic_Topic_ChildTopicId",
                        column: x => x.ChildTopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedTopic_Topic_ParentTopicId",
                        column: x => x.ParentTopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTopic_ChildTopicId",
                table: "RelatedTopic",
                column: "ChildTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTopic_ParentTopicId",
                table: "RelatedTopic",
                column: "ParentTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedTopic");
        }
    }
}
