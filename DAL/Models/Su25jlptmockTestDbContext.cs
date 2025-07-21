using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Models;

public partial class Su25jlptmockTestDbContext : DbContext
{
    public Su25jlptmockTestDbContext()
    {
    }

    public Su25jlptmockTestDbContext(DbContextOptions<Su25jlptmockTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Jlptaccount> Jlptaccounts { get; set; }

    public virtual DbSet<MockTest> MockTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539BFCA72009BB");

            entity.ToTable("Candidate");

            entity.Property(e => e.CandidateId)
                .ValueGeneratedNever()
                .HasColumnName("CandidateID");
            entity.Property(e => e.FullName).HasMaxLength(120);
            entity.Property(e => e.Jlptlevel)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("JLPTLevel");
            entity.Property(e => e.StudyGoal).HasMaxLength(255);
        });

        modelBuilder.Entity<Jlptaccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__JLPTAcco__349DA5868975E6D8");

            entity.ToTable("JLPTAccount");

            entity.HasIndex(e => e.Email, "UQ__JLPTAcco__A9D10534215A3E3A").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(60);
        });

        modelBuilder.Entity<MockTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__MockTest__8CC331009438ACFC");

            entity.ToTable("MockTest");

            entity.Property(e => e.TestId)
                .ValueGeneratedNever()
                .HasColumnName("TestID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.SkillArea).HasMaxLength(50);
            entity.Property(e => e.TestTitle).HasMaxLength(150);

            entity.HasOne(d => d.Candidate).WithMany(p => p.MockTests)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MockTest__Candid__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
