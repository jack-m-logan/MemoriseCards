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
        public DbSet<User> User { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Deck> Deck { get; set; }
        public DbSet<POA> POA { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<Score> Score { get; set; }

        public MemoriseCardsDbContext()
        {
        }

        public MemoriseCardsDbContext(DbContextOptions<MemoriseCardsDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string not found in environment variables.");
                }
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {  
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Username).HasColumnName("Username").IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasColumnName("PasswordHash").IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordSalt).HasColumnName("PasswordSalt").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Suit).HasColumnName("Suit").IsRequired().HasMaxLength(10);
                entity.Property(e => e.Rank).HasColumnName("Rank").IsRequired().HasMaxLength(10);
                entity.Property(e => e.POAId).HasColumnName("POAId").HasMaxLength(255);
                entity.Property(e => e.TotalCardScore).HasColumnName("TotalCardScore");
                entity.HasIndex(e => new { e.Id, e.Suit, e.Rank }).IsUnique();
                entity.HasOne(e => e.POA)
                      .WithOne()
                      .HasForeignKey<Card>(e => e.POAId);
            });

            modelBuilder.Entity<Deck>(entity =>
            {
                entity.ToTable("Deck");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(255);
                entity.Property(e => e.Notes).HasColumnName("Notes").HasColumnType("TEXT");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Decks)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.SetNull); 
            });
            
            modelBuilder.Entity<POA>(entity =>
            {
                entity.ToTable("POA");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.CardId).HasColumnName("CardId").IsRequired();
                entity.Property(e => e.Person).HasColumnName("Person").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Object).HasColumnName("Object").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Action).HasColumnName("Action").IsRequired().HasMaxLength(255);
                entity.HasIndex(e => new { e.Person, e.Object, e.Action, e.CardId }).IsUnique();
                entity.HasOne(e => e.Card)
                      .WithOne()
                      .HasForeignKey<POA>(e => e.CardId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("Round");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.DeckId).HasColumnName("DeckId").IsRequired();
                entity.Property(e => e.RoundNumber).HasColumnName("RoundNumber").IsRequired();
                entity.HasIndex(e => e.RoundNumber).IsUnique();
                entity.HasOne(e => e.Deck)
                      .WithMany()
                      .HasForeignKey(e => e.DeckId)
                      .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<Score>(entity =>
            {
                entity.ToTable("Score");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Ratio).HasColumnName("Ratio").HasColumnType("float");
                entity.HasIndex(e => new { e.UserId, e.RoundId }).IsUnique();
                entity.HasOne(e => e.Round)
                      .WithMany(e => e.Scores)
                      .HasForeignKey(e => e.RoundId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }

        public void InitializeDatabase()
        {
            if (Database.CanConnect())
            {
                if (!Database.IsRelational())
                {
                    throw new Exception("Cannot use EnsureCreated with a non-relational database.");
                }

                if (!Database.EnsureCreated())
                {
                    throw new Exception("Could not ensure the database was created.");
                }
            }
        }
    }
}

