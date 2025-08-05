using Microsoft.EntityFrameworkCore;
using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;


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

        public DbSet<ReportColumn> ReportColumns { get; set; }

        public DbSet<ReportParameter> ReportParameters { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentDetail> StudentDetails { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert all table and column names to uppercase (unquoted)
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Set table name to uppercase
                entity.SetTableName(entity.GetTableName().ToUpper());

                // Convert all column names to uppercase
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToUpper());
                }
            }

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



        }
    }
}


