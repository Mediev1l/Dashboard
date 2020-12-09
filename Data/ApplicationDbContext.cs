using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBoard.Models.BoardModels;
using MyBoard.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyBoard.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<AppUser>(u =>
            //{
            //    u.Property(i => i.Id).HasDefaultValueSql("newsequentialid()");
            //});

            //builder.Entity<AppRole>(r =>
            //{
            //    r.Property(i => i.Id).HasDefaultValueSql("newsequentialid()");
            //});

            builder.Entity<UserBoard>()
                .HasKey(e => new { e.UserId, e.BoardId });

            builder.Entity<UserBoard>()
                .HasOne(o => o.User)
                .WithMany(m => m.Boards)
                .HasForeignKey(f => f.UserId);

            builder.Entity<UserBoard>()
                .HasOne(o => o.Board)
                .WithMany(m => m.Users)
                .HasForeignKey(f => f.BoardId);


        }

    }
}
