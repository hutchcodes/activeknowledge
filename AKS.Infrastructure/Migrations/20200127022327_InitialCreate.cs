using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AKS.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ParentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => new { x.CategoryId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_Category_Category",
                        columns: x => new { x.ParentCategoryId, x.ProjectId },
                        principalTable: "Category",
                        principalColumns: new[] { "CategoryId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LogoFileName = table.Column<string>(nullable: true),
                    CustomCssId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => new { x.TagId, x.ProjectId });
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TopicType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    TopicStatus = table.Column<int>(nullable: false),
                    ImageResourceId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    FileResourceId = table.Column<Guid>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    DefaultCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => new { x.TopicId, x.ProjectId });
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    GroupName = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CanEditCustomer = table.Column<bool>(nullable: false),
                    CanManageAccess = table.Column<bool>(nullable: false),
                    CanCreateProject = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LogoFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTopic",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTopic", x => new { x.ProjectId, x.CategoryId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_CategoryTopic_Category",
                        columns: x => new { x.CategoryId, x.ProjectId },
                        principalTable: "Category",
                        principalColumns: new[] { "CategoryId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryTopic_Topic",
                        columns: x => new { x.TopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectionElement",
                columns: table => new
                {
                    CollectionElementId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionElement", x => new { x.CollectionElementId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_CollectionElement_Topic",
                        columns: x => new { x.TopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

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
                        name: "FK_RelatedTopic_Topic",
                        columns: x => new { x.ChildTopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedTopic_ParentTopic",
                        columns: x => new { x.ParentTopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

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
                        name: "FK_Fragment_Topic",
                        columns: x => new { x.ChildTopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topic_Fragment",
                        columns: x => new { x.ParentTopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicTag",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTag", x => new { x.ProjectId, x.TopicId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TopicTag_Tag",
                        columns: x => new { x.TagId, x.ProjectId },
                        principalTable: "Tag",
                        principalColumns: new[] { "TagId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicTag_Topic",
                        columns: x => new { x.TopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupProjectPermissions",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    CanRead = table.Column<bool>(nullable: false),
                    CanEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupProjectPermissions", x => new { x.GroupId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_GroupProjectPermission_Group",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Group",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UerGroup_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectionElementTopic",
                columns: table => new
                {
                    CollectionElementId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionElementTopic", x => new { x.ProjectId, x.CollectionElementId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_CollectionElementTopic_CollectionElement",
                        columns: x => new { x.CollectionElementId, x.ProjectId },
                        principalTable: "CollectionElement",
                        principalColumns: new[] { "CollectionElementId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectionElementTopic_Topic",
                        columns: x => new { x.TopicId, x.ProjectId },
                        principalTable: "Topic",
                        principalColumns: new[] { "TopicId", "ProjectId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId_ProjectId",
                table: "Category",
                columns: new[] { "ParentCategoryId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTopic_CategoryId",
                table: "CategoryTopic",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTopic_TopicId",
                table: "CategoryTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTopic_CategoryId_ProjectId",
                table: "CategoryTopic",
                columns: new[] { "CategoryId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTopic_TopicId_ProjectId",
                table: "CategoryTopic",
                columns: new[] { "TopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElement_TopicId",
                table: "CollectionElement",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElement_TopicId_ProjectId",
                table: "CollectionElement",
                columns: new[] { "TopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_CollectionElementId",
                table: "CollectionElementTopic",
                column: "CollectionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_TopicId",
                table: "CollectionElementTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_CollectionElementId_ProjectId",
                table: "CollectionElementTopic",
                columns: new[] { "CollectionElementId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionElementTopic_TopicId_ProjectId",
                table: "CollectionElementTopic",
                columns: new[] { "TopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CustomerId",
                table: "Groups",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTopic_ChildTopicId",
                table: "RelatedTopic",
                column: "ChildTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTopic_ParentTopicId",
                table: "RelatedTopic",
                column: "ParentTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTopic_ChildTopicId_ProjectId",
                table: "RelatedTopic",
                columns: new[] { "ChildTopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTopic_ParentTopicId_ProjectId",
                table: "RelatedTopic",
                columns: new[] { "ParentTopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TopicFragment_ChildTopicId",
                table: "TopicFragment",
                column: "ChildTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicFragment_ParentTopicId",
                table: "TopicFragment",
                column: "ParentTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicFragment_ChildTopicId_ProjectId",
                table: "TopicFragment",
                columns: new[] { "ChildTopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TopicFragment_ParentTopicId_ProjectId",
                table: "TopicFragment",
                columns: new[] { "ParentTopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TopicTag_TagId_ProjectId",
                table: "TopicTag",
                columns: new[] { "TagId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TopicTag_TopicId_ProjectId",
                table: "TopicTag",
                columns: new[] { "TopicId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerId",
                table: "Users",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTopic");

            migrationBuilder.DropTable(
                name: "CollectionElementTopic");

            migrationBuilder.DropTable(
                name: "GroupProjectPermissions");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "RelatedTopic");

            migrationBuilder.DropTable(
                name: "TopicFragment");

            migrationBuilder.DropTable(
                name: "TopicTag");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CollectionElement");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
