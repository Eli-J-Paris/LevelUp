using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LevelUp.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    xp_reward = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false),
                    max_score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "camps",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    owner = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_camps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    favorite_animal = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    xp = table.Column<int>(type: "integer", nullable: false),
                    hygiene = table.Column<int>(type: "integer", nullable: false),
                    wellness = table.Column<int>(type: "integer", nullable: false),
                    mindfullness = table.Column<int>(type: "integer", nullable: false),
                    productivity = table.Column<int>(type: "integer", nullable: false),
                    habit_building = table.Column<int>(type: "integer", nullable: false),
                    pfp_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "all_achievements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hygenie5achievement_id = table.Column<int>(type: "integer", nullable: false),
                    wellness5achievement_id = table.Column<int>(type: "integer", nullable: false),
                    mindfulness5achievement_id = table.Column<int>(type: "integer", nullable: false),
                    productivity5achievement_id = table.Column<int>(type: "integer", nullable: false),
                    habit_building5achievement_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_all_achievements", x => x.id);
                    table.ForeignKey(
                        name: "fk_all_achievements_achievements_habit_building5achievement_id",
                        column: x => x.habit_building5achievement_id,
                        principalTable: "achievements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_all_achievements_achievements_hygenie5achievement_id",
                        column: x => x.hygenie5achievement_id,
                        principalTable: "achievements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_all_achievements_achievements_mindfulness5achievement_id",
                        column: x => x.mindfulness5achievement_id,
                        principalTable: "achievements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_all_achievements_achievements_productivity5achievement_id",
                        column: x => x.productivity5achievement_id,
                        principalTable: "achievements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_all_achievements_achievements_wellness5achievement_id",
                        column: x => x.wellness5achievement_id,
                        principalTable: "achievements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_all_achievements_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "camp_user",
                columns: table => new
                {
                    camps_id = table.Column<int>(type: "integer", nullable: false),
                    members_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_camp_user", x => new { x.camps_id, x.members_id });
                    table.ForeignKey(
                        name: "fk_camp_user_camps_camps_id",
                        column: x => x.camps_id,
                        principalTable: "camps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_camp_user_users_members_id",
                        column: x => x.members_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "daily_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    task_type = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    difficulty = table.Column<int>(type: "integer", nullable: false),
                    xp_reward = table.Column<int>(type: "integer", nullable: false),
                    attribute_reward = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    time_completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    streak = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_daily_tasks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    camp_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_camps_camp_id",
                        column: x => x.camp_id,
                        principalTable: "camps",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "to_do_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    task_type = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    difficulty = table.Column<int>(type: "integer", nullable: false),
                    xp_reward = table.Column<int>(type: "integer", nullable: false),
                    attribute_reward = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    time_completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_to_do_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_to_do_tasks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "weekly_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    task_type = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    difficulty = table.Column<int>(type: "integer", nullable: false),
                    xp_reward = table.Column<int>(type: "integer", nullable: false),
                    attribute_reward = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    time_completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    streak = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_weekly_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_weekly_tasks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_all_achievements_habit_building5achievement_id",
                table: "all_achievements",
                column: "habit_building5achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_all_achievements_hygenie5achievement_id",
                table: "all_achievements",
                column: "hygenie5achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_all_achievements_mindfulness5achievement_id",
                table: "all_achievements",
                column: "mindfulness5achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_all_achievements_productivity5achievement_id",
                table: "all_achievements",
                column: "productivity5achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_all_achievements_user_id",
                table: "all_achievements",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_all_achievements_wellness5achievement_id",
                table: "all_achievements",
                column: "wellness5achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_camp_user_members_id",
                table: "camp_user",
                column: "members_id");

            migrationBuilder.CreateIndex(
                name: "ix_daily_tasks_user_id",
                table: "daily_tasks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_camp_id",
                table: "messages",
                column: "camp_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_user_id",
                table: "messages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_to_do_tasks_user_id",
                table: "to_do_tasks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_weekly_tasks_user_id",
                table: "weekly_tasks",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "all_achievements");

            migrationBuilder.DropTable(
                name: "camp_user");

            migrationBuilder.DropTable(
                name: "daily_tasks");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "to_do_tasks");

            migrationBuilder.DropTable(
                name: "weekly_tasks");

            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "camps");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
