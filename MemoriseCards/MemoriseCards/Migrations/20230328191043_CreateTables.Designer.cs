﻿// <auto-generated />
using System;
using MemoriseCards.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MemoriseCards.Migrations
{
    [DbContext(typeof(MemoriseCardsDbContext))]
    [Migration("20230328191043_CreateTables")]
    partial class CreateTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MemoriseCards.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeckId")
                        .HasColumnType("integer");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("Rank");

                    b.Property<string>("Suit")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("Suit");

                    b.Property<float>("TotalCardScore")
                        .HasColumnType("real")
                        .HasColumnName("TotalCardScore");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.HasIndex("Suit", "Rank")
                        .IsUnique();

                    b.ToTable("Card", (string)null);
                });

            modelBuilder.Entity("MemoriseCards.Models.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Name");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Notes");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Deck", (string)null);
                });

            modelBuilder.Entity("MemoriseCards.Models.POA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Action");

                    b.Property<int?>("CardID")
                        .HasColumnType("integer");

                    b.Property<int>("CardId")
                        .HasColumnType("integer")
                        .HasColumnName("CardId");

                    b.Property<string>("Object")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Object");

                    b.Property<string>("Person")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Person");

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.HasIndex("Person", "Object", "Action", "CardId")
                        .IsUnique();

                    b.ToTable("POA", (string)null);
                });

            modelBuilder.Entity("MemoriseCards.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DeckId")
                        .HasColumnType("integer")
                        .HasColumnName("DeckId");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("integer")
                        .HasColumnName("RoundNumber");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.HasIndex("RoundNumber")
                        .IsUnique();

                    b.ToTable("Round", (string)null);
                });

            modelBuilder.Entity("MemoriseCards.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Ratio")
                        .HasColumnType("float")
                        .HasColumnName("Ratio");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<int>("TotalScore")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.HasIndex("UserId", "RoundId")
                        .IsUnique();

                    b.ToTable("Score", (string)null);
                });

            modelBuilder.Entity("MemoriseCards.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("LastName");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("PasswordSalt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MemoriseCards.Models.Card", b =>
                {
                    b.HasOne("MemoriseCards.Models.Deck", null)
                        .WithMany("Cards")
                        .HasForeignKey("DeckId");
                });

            modelBuilder.Entity("MemoriseCards.Models.Deck", b =>
                {
                    b.HasOne("MemoriseCards.Models.User", "User")
                        .WithMany("Decks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MemoriseCards.Models.POA", b =>
                {
                    b.HasOne("MemoriseCards.Models.Card", "Card")
                        .WithOne()
                        .HasForeignKey("MemoriseCards.Models.POA", "CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("MemoriseCards.Models.Round", b =>
                {
                    b.HasOne("MemoriseCards.Models.Deck", "Deck")
                        .WithMany()
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("MemoriseCards.Models.Score", b =>
                {
                    b.HasOne("MemoriseCards.Models.Round", "Round")
                        .WithMany("Scores")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemoriseCards.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Round");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MemoriseCards.Models.Deck", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("MemoriseCards.Models.Round", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("MemoriseCards.Models.User", b =>
                {
                    b.Navigation("Decks");
                });
#pragma warning restore 612, 618
        }
    }
}
