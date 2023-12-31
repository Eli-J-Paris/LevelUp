﻿// <auto-generated />
using System;
using LevelUp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LevelUp.Migrations
{
    [DbContext(typeof(LevelUpContext))]
    [Migration("20231106222522_tet")]
    partial class tet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CampUser", b =>
                {
                    b.Property<int>("CampsId")
                        .HasColumnType("integer")
                        .HasColumnName("camps_id");

                    b.Property<int>("MembersId")
                        .HasColumnType("integer")
                        .HasColumnName("members_id");

                    b.HasKey("CampsId", "MembersId")
                        .HasName("pk_camp_user");

                    b.HasIndex("MembersId")
                        .HasDatabaseName("ix_camp_user_members_id");

                    b.ToTable("camp_user", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<int>("MaxScore")
                        .HasColumnType("integer")
                        .HasColumnName("max_score");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("XpReward")
                        .HasColumnType("integer")
                        .HasColumnName("xp_reward");

                    b.HasKey("Id")
                        .HasName("pk_achievements");

                    b.ToTable("achievements", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.AchievementHandler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("HabitBuilding5AchievementId")
                        .HasColumnType("integer")
                        .HasColumnName("habit_building5achievement_id");

                    b.Property<int>("Hygenie5AchievementId")
                        .HasColumnType("integer")
                        .HasColumnName("hygenie5achievement_id");

                    b.Property<int>("Mindfulness5AchievementId")
                        .HasColumnType("integer")
                        .HasColumnName("mindfulness5achievement_id");

                    b.Property<int>("Productivity5AchievementId")
                        .HasColumnType("integer")
                        .HasColumnName("productivity5achievement_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("Wellness5AchievementId")
                        .HasColumnType("integer")
                        .HasColumnName("wellness5achievement_id");

                    b.HasKey("Id")
                        .HasName("pk_all_achievements");

                    b.HasIndex("HabitBuilding5AchievementId")
                        .HasDatabaseName("ix_all_achievements_habit_building5achievement_id");

                    b.HasIndex("Hygenie5AchievementId")
                        .HasDatabaseName("ix_all_achievements_hygenie5achievement_id");

                    b.HasIndex("Mindfulness5AchievementId")
                        .HasDatabaseName("ix_all_achievements_mindfulness5achievement_id");

                    b.HasIndex("Productivity5AchievementId")
                        .HasDatabaseName("ix_all_achievements_productivity5achievement_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_all_achievements_user_id");

                    b.HasIndex("Wellness5AchievementId")
                        .HasDatabaseName("ix_all_achievements_wellness5achievement_id");

                    b.ToTable("all_achievements", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.Camp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("owner");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_camps");

                    b.ToTable("camps", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.DailyTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeReward")
                        .HasColumnType("integer")
                        .HasColumnName("attribute_reward");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<int>("Streak")
                        .HasColumnType("integer")
                        .HasColumnName("streak");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("task_type");

                    b.Property<DateTime?>("TimeCompleted")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_completed");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_created");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("XpReward")
                        .HasColumnType("integer")
                        .HasColumnName("xp_reward");

                    b.HasKey("Id")
                        .HasName("pk_daily_tasks");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_daily_tasks_user_id");

                    b.ToTable("daily_tasks", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CampId")
                        .HasColumnType("integer")
                        .HasColumnName("camp_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_messages");

                    b.HasIndex("CampId")
                        .HasDatabaseName("ix_messages_camp_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_messages_user_id");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.ToDoTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeReward")
                        .HasColumnType("integer")
                        .HasColumnName("attribute_reward");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("task_type");

                    b.Property<DateTime?>("TimeCompleted")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_completed");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_created");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("XpReward")
                        .HasColumnType("integer")
                        .HasColumnName("xp_reward");

                    b.HasKey("Id")
                        .HasName("pk_to_do_tasks");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_to_do_tasks_user_id");

                    b.ToTable("to_do_tasks", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FavoriteAnimal")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("favorite_animal");

                    b.Property<int>("HabitBuilding")
                        .HasColumnType("integer")
                        .HasColumnName("habit_building");

                    b.Property<int>("Hygiene")
                        .HasColumnType("integer")
                        .HasColumnName("hygiene");

                    b.Property<int>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<int>("Mindfullness")
                        .HasColumnType("integer")
                        .HasColumnName("mindfullness");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(747)
                        .HasColumnType("character varying(747)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PfpUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("pfp_url");

                    b.Property<int>("Productivity")
                        .HasColumnType("integer")
                        .HasColumnName("productivity");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("username");

                    b.Property<int>("Wellness")
                        .HasColumnType("integer")
                        .HasColumnName("wellness");

                    b.Property<int>("Xp")
                        .HasColumnType("integer")
                        .HasColumnName("xp");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("LevelUp.Models.WeeklyTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeReward")
                        .HasColumnType("integer")
                        .HasColumnName("attribute_reward");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<int>("Streak")
                        .HasColumnType("integer")
                        .HasColumnName("streak");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("task_type");

                    b.Property<DateTime?>("TimeCompleted")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_completed");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_created");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("XpReward")
                        .HasColumnType("integer")
                        .HasColumnName("xp_reward");

                    b.HasKey("Id")
                        .HasName("pk_weekly_tasks");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_weekly_tasks_user_id");

                    b.ToTable("weekly_tasks", (string)null);
                });

            modelBuilder.Entity("CampUser", b =>
                {
                    b.HasOne("LevelUp.Models.Camp", null)
                        .WithMany()
                        .HasForeignKey("CampsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_camp_user_camps_camps_id");

                    b.HasOne("LevelUp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_camp_user_users_members_id");
                });

            modelBuilder.Entity("LevelUp.Models.AchievementHandler", b =>
                {
                    b.HasOne("LevelUp.Models.Achievement", "HabitBuilding5Achievement")
                        .WithMany()
                        .HasForeignKey("HabitBuilding5AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_all_achievements_achievements_habit_building5achievement_id");

                    b.HasOne("LevelUp.Models.Achievement", "Hygenie5Achievement")
                        .WithMany()
                        .HasForeignKey("Hygenie5AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_all_achievements_achievements_hygenie5achievement_id");

                    b.HasOne("LevelUp.Models.Achievement", "Mindfulness5Achievement")
                        .WithMany()
                        .HasForeignKey("Mindfulness5AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_all_achievements_achievements_mindfulness5achievement_id");

                    b.HasOne("LevelUp.Models.Achievement", "Productivity5Achievement")
                        .WithMany()
                        .HasForeignKey("Productivity5AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_all_achievements_achievements_productivity5achievement_id");

                    b.HasOne("LevelUp.Models.User", null)
                        .WithOne("Achievements")
                        .HasForeignKey("LevelUp.Models.AchievementHandler", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_all_achievements_users_user_id");

                    b.HasOne("LevelUp.Models.Achievement", "Wellness5Achievement")
                        .WithMany()
                        .HasForeignKey("Wellness5AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_all_achievements_achievements_wellness5achievement_id");

                    b.Navigation("HabitBuilding5Achievement");

                    b.Navigation("Hygenie5Achievement");

                    b.Navigation("Mindfulness5Achievement");

                    b.Navigation("Productivity5Achievement");

                    b.Navigation("Wellness5Achievement");
                });

            modelBuilder.Entity("LevelUp.Models.DailyTask", b =>
                {
                    b.HasOne("LevelUp.Models.User", "User")
                        .WithMany("DailyTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_daily_tasks_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LevelUp.Models.Message", b =>
                {
                    b.HasOne("LevelUp.Models.Camp", null)
                        .WithMany("MessageBoard")
                        .HasForeignKey("CampId")
                        .HasConstraintName("fk_messages_camps_camp_id");

                    b.HasOne("LevelUp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_messages_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LevelUp.Models.ToDoTask", b =>
                {
                    b.HasOne("LevelUp.Models.User", "User")
                        .WithMany("ToDoTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_to_do_tasks_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LevelUp.Models.WeeklyTask", b =>
                {
                    b.HasOne("LevelUp.Models.User", "User")
                        .WithMany("WeeklyTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_weekly_tasks_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LevelUp.Models.Camp", b =>
                {
                    b.Navigation("MessageBoard");
                });

            modelBuilder.Entity("LevelUp.Models.User", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("DailyTasks");

                    b.Navigation("ToDoTasks");

                    b.Navigation("WeeklyTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
