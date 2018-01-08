using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuzzleStore.Data.Models;

namespace PuzzleStore.Data
{
    public class PuzzleStoreDbContext : IdentityDbContext<User>
    {
        public PuzzleStoreDbContext(DbContextOptions<PuzzleStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Guide> Guides { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Puzzles)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.Entity<Part>()
                .HasOne(p => p.Puzzle)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.PuzzleId);

            builder.Entity<Part>()
                .HasOne(p => p.User)
                .WithMany(u => u.Parts)
                .HasForeignKey(p => p.UserId);

            base.OnModelCreating(builder);
        }
    }
}
