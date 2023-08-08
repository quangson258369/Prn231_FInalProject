using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class PrnYarnTowelManagementContext : DbContext
{
    public PrnYarnTowelManagementContext()
    {
    }

    public PrnYarnTowelManagementContext(DbContextOptions<PrnYarnTowelManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-MOH7P06\\SQLEXPRESS; database=prn_yarn_towel_management;uid=sa;pwd=12345678;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CD13423DA5");

            entity.ToTable("account");

            entity.HasIndex(e => e.AccountUsername, "UQ__account__4F5C27E7542FF1CF").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountPassword)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("account_password");
            entity.Property(e => e.AccountUsername)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("account_username");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");

            entity.HasMany(d => d.Roles).WithMany(p => p.Accounts)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleAccount",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__role_acco__role___3A81B327"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__role_acco__accou__38996AB5"),
                    j =>
                    {
                        j.HasKey("AccountId", "RoleId").HasName("PK__role_acc__91C2B491CE23BFEC");
                        j.ToTable("role_account");
                        j.IndexerProperty<int>("AccountId").HasColumnName("account_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__47027DF59CBB454A");

            entity.ToTable("product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__760965CC3FF044AF");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__transact__85C600AF487CE9A6");

            entity.ToTable("transaction");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.DateCreated)
                .HasColumnType("date")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.TotalPaid)
                .HasColumnType("money")
                .HasColumnName("total_paid");
            entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("FK__transacti__trans__3C69FB99");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__transacti__user___3E52440B");
        });

        modelBuilder.Entity<TransactionDetail>(entity =>
        {
            entity.ToTable("transaction_detail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.Product).WithMany(p => p.TransactionDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__transacti__produ__2B3F6F97");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionDetails)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__transacti__trans__2C3393D0");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__transact__439ABFC1EE971DCE");

            entity.ToTable("transaction_type");

            entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
            entity.Property(e => e.TransactionTypeName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("transaction_type_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370FD8C44433");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UserAddress)
                .HasMaxLength(200)
                .HasColumnName("user_address");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(20)
                .HasColumnName("user_phone");
            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK__user__user_type___4222D4EF");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PK__user_typ__9424CFA64EEA3998");

            entity.ToTable("user_type");

            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");
            entity.Property(e => e.UserTypeName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("user_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
