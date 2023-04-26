using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace movie_collection.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        title = table.Column<string>(type: "text", nullable: false),
                        description = table.Column<string>(type: "text", nullable: false),
                        rating = table.Column<int>(type: "integer", nullable: false),
                        release_date = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        inventory_date = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movies", x => x.id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "movies");
        }
    }
}
