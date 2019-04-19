using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AKS.Infrastructure.Migrations
{
    public partial class AutoMapper2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topic_CollectionElement_CollectionElementId",
                table: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Topic_CollectionElementId",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CollectionElementId",
                table: "Topic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                columns: new[] { "TagId", "ProjectId" });

            migrationBuilder.CreateTable(
                name: "CollectionElementTopic",
                columns: table => new
                {
                    CollectionElementId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CollectionElementId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionElementTopic", x => new { x.ProjectId, x.CollectionElementId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_CollectionElementTopic_CollectionElement_CollectionElementId",
                        column: x => x.CollectionElementId,
                        principalTable: "CollectionElement",
                        principalColumn: "CollectionElementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectionElementTopic_CollectionElement_CollectionElementId1",
                        column: x => x.CollectionElementId1,
                        principalTable: "CollectionElement",
                        principalColumn: "CollectionElementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectionElementTopic_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_CollectionElementId",
                table: "CollectionElementTopic",
                column: "CollectionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_CollectionElementId1",
                table: "CollectionElementTopic",
                column: "CollectionElementId1");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_TopicId",
                table: "CollectionElementTopic",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionElementTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionElementId",
                table: "Topic",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_CollectionElementId",
                table: "Topic",
                column: "CollectionElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_CollectionElement_CollectionElementId",
                table: "Topic",
                column: "CollectionElementId",
                principalTable: "CollectionElement",
                principalColumn: "CollectionElementId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
