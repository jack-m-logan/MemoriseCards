using System;
using MemoriseCards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using static System.Formats.Asn1.AsnWriter;

namespace MemoriseCards.Data
{
    public class MemoriseCardsDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserModel> User { get; set; }
        public DbSet<CardModel> Card { get; set; }
        public DbSet<DeckModel> Deck { get; set; }
        public DbSet<POAModel> POA { get; set; }
        public DbSet<RoundModel> Round { get; set; }
        public DbSet<ScoreModel> Score { get; set; }

        public MemoriseCardsDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string for your database
            optionsBuilder.UseNpgsql("ConnectionString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").IsRequired();
                entity.Property(e => e.Username).HasColumnName("Username").IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasColumnName("PasswordHash").IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordSalt).HasColumnName("PasswordSalt").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<CardModel>(entity =>
            {
                entity.ToTable("Card");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Suit).HasColumnName("Suit").IsRequired().HasMaxLength(10);
                entity.Property(e => e.Rank).HasColumnName("Rank").IsRequired();
                entity.Property(e => e.TotalCardScore).HasColumnName("TotalCardScore");
                entity.HasIndex(e => new { e.Suit, e.Rank }).IsUnique();
            });

            modelBuilder.Entity<DeckModel>(entity =>
            {
                entity.ToTable("Deck");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Name).HasColumnName("Name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Notes).HasColumnName("Notes").HasColumnType("TEXT");
                entity.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Decks)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade); 
            });
            
            modelBuilder.Entity<POAModel>(entity =>
            {
                entity.ToTable("POA");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.CardId).HasColumnName("CardId").IsRequired();
                entity.Property(e => e.Person).HasColumnName("Person").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Object).HasColumnName("Object").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Action).HasColumnName("Action").IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.CardId);
                entity.HasIndex(e => new { e.Person, e.Object, e.Action, e.CardId }).IsUnique();
                entity.HasOne(e => e.Card)
                      .WithOne()
                      .HasForeignKey<POAModel>(e => e.CardId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RoundModel>(entity =>
            {
                entity.ToTable("Round");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.DeckId).HasColumnName("DeckId").IsRequired();
                entity.Property(e => e.RoundNumber).HasColumnName("RoundNumber").IsRequired();
                entity.HasIndex(e => e.RoundNumber).IsUnique();
                entity.HasOne(e => e.Deck)
                      .WithMany()
                      .HasForeignKey(e => e.DeckId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<ScoreModel>(entity =>
            {
                entity.ToTable("Score");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Ratio).HasColumnName("Ratio").HasColumnType("float");
                entity.HasIndex(e => new { e.UserId, e.RoundId }).IsUnique();
                entity.HasOne(e => e.Round)
                      .WithMany(e => e.Scores)
                      .HasForeignKey(e => e.RoundId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

