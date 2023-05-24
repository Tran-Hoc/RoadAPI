using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoadAPI.Data;

namespace RoadAPI.DataContext;

public partial class RoadAndOtherApiContext : DbContext
{
    public RoadAndOtherApiContext()
    {
    }

    public RoadAndOtherApiContext(DbContextOptions<RoadAndOtherApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<ImageReport> ImageReports { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-7MRL7G7\\SQLEXPRESS;Initial Catalog=Road_and_other_API;Integrated Security=True; TrustServerCertificate=True; User ID=sa;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ImageReport>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Reports).HasConstraintName("FK_report_account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
