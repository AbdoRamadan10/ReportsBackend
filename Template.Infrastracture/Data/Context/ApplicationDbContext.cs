using Microsoft.EntityFrameworkCore;
using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReportsBackend.Infrastracture.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<RoleScreen> RoleScreens { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<RoleReport> RoleReports { get; set; }
        public DbSet<Privilege> Privileges { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RoleReport>().HasKey(ur => new { ur.ReportId, ur.RoleId });

            modelBuilder.Entity<RoleReport>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.RoleReports)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RoleReport>()
                .HasOne(ur => ur.Report)
                .WithMany(r => r.RoleReports)
                .HasForeignKey(ur => ur.ReportId);


            modelBuilder.Entity<RoleScreen>().HasKey(ur => new { ur.ScreenId, ur.RoleId });

            modelBuilder.Entity<RoleScreen>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.RoleScreens)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RoleScreen>()
                .HasOne(ur => ur.Screen)
                .WithMany(r => r.RoleScreens)
                .HasForeignKey(ur => ur.ScreenId);


            // Seed Actions
            modelBuilder.Entity<Privilege>().HasData(
                new Privilege { Id = 1, Name = "View" },
                new Privilege { Id = 2, Name = "Export" },
                new Privilege { Id = 3, Name = "Edit" },
                new Privilege { Id = 4, Name = "Print" }
            );
        }
    }
}


