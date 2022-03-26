using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using sdb.Models;

namespace sdb.Data
{
    public partial class sdbdb25janContext : DbContext
    {
        public sdbdb25janContext()
        {
        }

        public sdbdb25janContext(DbContextOptions<sdbdb25janContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SdbCompaignPurpose> SdbCompaignPurpose { get; set; }
        public virtual DbSet<SdbCompaigns> SdbCompaigns { get; set; }
        public virtual DbSet<SdbPayments> SdbPayments { get; set; }
        public virtual DbSet<SdbRoles> SdbRoles { get; set; }
        public virtual DbSet<SdbSystemUsers> SdbSystemUsers { get; set; }
        public virtual DbSet<SdbTransaction> SdbTransaction { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SdbCompaignPurpose>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Purpose })
                    .HasName("PK__sdb_comp__09A948F8003679B7");

                entity.ToTable("sdb_compaign_purpose");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Purpose)
                    .HasColumnName("purpose")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SdbCompaigns>(entity =>
            {
                entity.ToTable("sdb_compaigns");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnName("collectedAmount")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CompaignPurpose)
                    .HasColumnName("compaign_purpose")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmountNeeded)
                    .HasColumnName("totalAmountNeeded")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updatedBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<SdbPayments>(entity =>
            {
                entity.ToTable("sdb_payments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CompaignId).HasColumnName("compaignId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updatedBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Compaign)
                    .WithMany(p => p.SdbPayments)
                    .HasForeignKey(d => d.CompaignId)
                    .HasConstraintName("FK_sdb_payments_sdb_compaigns");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SdbPayments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_sdb_payments_sdb_system_users");
            });

            modelBuilder.Entity<SdbRoles>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Role })
                    .HasName("PK__sdb_role__BA703A2B6F16C7DC");

                entity.ToTable("sdb_roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SdbSystemUsers>(entity =>
            {
                entity.ToTable("sdb_system_users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updatedBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole)
                    .HasColumnName("userRole")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SdbTransaction>(entity =>
            {
                entity.ToTable("sdb_transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasColumnName("cardNumber")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CompaignId).HasColumnName("compaignId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Csv).HasColumnName("csv");

                entity.Property(e => e.DonationAmount)
                    .HasColumnName("donationAmount")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DonorId).HasColumnName("donorId");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiryDate")
                    .HasColumnType("date");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updatedBy")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Compaign)
                    .WithMany(p => p.SdbTransaction)
                    .HasForeignKey(d => d.CompaignId)
                    .HasConstraintName("FK_sdb_transaction_sdb_compaigns");

                entity.HasOne(d => d.Donor)
                    .WithMany(p => p.SdbTransaction)
                    .HasForeignKey(d => d.DonorId)
                    .HasConstraintName("FK_sdb_transaction_sdb_system_users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
