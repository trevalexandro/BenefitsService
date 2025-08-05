using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenefitsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropDependentRelationshipsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependents_DependentRelationships_RelationshipId",
                table: "Dependents");

            migrationBuilder.DropTable(
                name: "DependentRelationships");

            migrationBuilder.DropIndex(
                name: "IX_Dependents_RelationshipId",
                table: "Dependents");

            migrationBuilder.RenameColumn(
                name: "RelationshipId",
                table: "Dependents",
                newName: "Relationship");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Relationship",
                table: "Dependents",
                newName: "RelationshipId");

            migrationBuilder.CreateTable(
                name: "DependentRelationships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependentRelationships", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_RelationshipId",
                table: "Dependents",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependents_DependentRelationships_RelationshipId",
                table: "Dependents",
                column: "RelationshipId",
                principalTable: "DependentRelationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
