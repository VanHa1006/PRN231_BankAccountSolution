using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models;

public partial class BankAccountTypeContext : DbContext
{
    public BankAccountTypeContext()
    {
    }

    public BankAccountTypeContext(DbContextOptions<BankAccountTypeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:BankAccountTypeDB"];
        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__AccountT__516F03959A978945");

            entity.Property(e => e.TypeId)
                .HasMaxLength(20)
                .HasColumnName("TypeID");
            entity.Property(e => e.TypeDesc).HasMaxLength(250);
            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(80);
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BankAcco__349DA586891DF526");

            entity.Property(e => e.AccountId)
                .HasMaxLength(20)
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountName)
                .IsRequired()
                .HasMaxLength(120);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.OpenDate).HasColumnType("date");
            entity.Property(e => e.TypeId)
                .HasMaxLength(20)
                .HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__BankAccou__TypeI__5DCAEF64");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC60A9D039");

            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("UserID");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
