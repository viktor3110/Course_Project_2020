using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VideoRental
{
    public partial class VideoRentalContext : DbContext
    {
        public VideoRentalContext()
        {
        }

        public VideoRentalContext(DbContextOptions<VideoRentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DiscView> DiscView { get; set; }
        public virtual DbSet<Disc> Discs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<RentalRecord> RentalRecords { get; set; }
        public virtual DbSet<RentalView> RentalView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-NO2QHQ2;Integrated Security=True;Database=VideoRental;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasMaxLength(100);

                entity.Property(e => e.Pasport)
                    .IsRequired()
                    .HasMaxLength(9);
            });

            modelBuilder.Entity<DiscView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DiscView");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DateOfCreation).HasColumnType("date");

                entity.Property(e => e.DateOfRecord).HasColumnType("date");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MainActor)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TypeOfDisc)
                    .IsRequired()
                    .HasMaxLength(7);
            });

            modelBuilder.Entity<Disc>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DateOfCreation).HasColumnType("date");

                entity.Property(e => e.DateOfRecord).HasColumnType("date");

                entity.Property(e => e.MainActor)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TypeOfDisc)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany(p => p.Discs)
                    .HasForeignKey(d => d.Genre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Discs__Genre__2A4B4B5E");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfWorkStart).HasColumnType("date");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasMaxLength(100);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<RentalRecord>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfRent).HasColumnType("date");

                entity.Property(e => e.DateOfReturn).HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RentalRecords)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RentalRec__Clien__2D27B809");

                entity.HasOne(d => d.Disc)
                    .WithMany(p => p.RentalRecords)
                    .HasForeignKey(d => d.DiscId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RentalRec__DiscI__2E1BDC42");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RentalRecords)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RentalRec__Emplo__2F10007B");
            });

            modelBuilder.Entity<RentalView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RentalView");

                entity.Property(e => e.Client)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateOfRent).HasColumnType("date");

                entity.Property(e => e.DateOfReturn).HasColumnType("date");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
