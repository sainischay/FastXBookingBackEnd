﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FastXBookingSample.Models
{
    public partial class BookingContext : DbContext
    {
        public BookingContext()
        {
        }

        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amenity> Amenities { get; set; } = null!;
        public virtual DbSet<BoardingPoint> BoardingPoints { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingHistory> BookingHistories { get; set; } = null!;
        public virtual DbSet<Bus> Buses { get; set; } = null!;
        public virtual DbSet<BusAmenity> BusAmenities { get; set; } = null!;
        public virtual DbSet<BusSeat> BusSeats { get; set; } = null!;
        public virtual DbSet<DroppingPoint> DroppingPoints { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-C8908545;Initial Catalog=Booking;Integrated Security=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.ToTable("Amenity");

                entity.Property(e => e.AmenityName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BoardingPoint>(entity =>
            {
                entity.HasKey(e => e.BoardingId)
                    .HasName("PK__Boarding__057071EA38123087");

                entity.Property(e => e.PlaceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.BoardingPoints)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__BoardingP__BusId__534D60F1");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.HasOne(d => d.Boarding)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BoardingId)
                    .HasConstraintName("FK_Booking_BoardingId");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__Booking__BusId__619B8048");

                entity.HasOne(d => d.Dropping)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.DroppingId)
                    .HasConstraintName("FK_Booking_DroppingId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Booking__UserId__60A75C0F");
            });

            modelBuilder.Entity<BookingHistory>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK__BookingH__3DE0C20740FA3004");

                entity.ToTable("BookingHistory");

                entity.Property(e => e.BookingDateTime).HasColumnType("datetime");

                entity.Property(e => e.BusName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BusNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Seats)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__BookingHi__Booki__6FE99F9F");
            });

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.ToTable("Bus");

                entity.Property(e => e.BusName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BusNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BusType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.Destination)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusOperatorNavigation)
                    .WithMany(p => p.Buses)
                    .HasForeignKey(d => d.BusOperator)
                    .HasConstraintName("FK__Bus__BusOperator__4D94879B");
            });

            modelBuilder.Entity<BusAmenity>(entity =>
            {
                entity.ToTable("Bus_Amenities");

                entity.HasOne(d => d.Amenity)
                    .WithMany(p => p.BusAmenities)
                    .HasForeignKey(d => d.AmenityId)
                    .HasConstraintName("FK__Bus_Ameni__Ameni__73BA3083");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.BusAmenities)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__Bus_Ameni__BusId__72C60C4A");
            });

            modelBuilder.Entity<BusSeat>(entity =>
            {
                entity.HasKey(e => e.SeatId)
                    .HasName("PK__BusSeats__311713F37D9D17DE");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.BusSeats)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__BusSeats__BusId__5070F446");
            });

            modelBuilder.Entity<DroppingPoint>(entity =>
            {
                entity.HasKey(e => e.DroppingId)
                    .HasName("PK__Dropping__DCC8059A043CD558");

                entity.Property(e => e.PlaceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.DroppingPoints)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__DroppingP__BusId__5629CD9C");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("Route");

                entity.Property(e => e.PlaceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__Route__BusId__59063A47");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.BookingDateTime).HasColumnType("datetime");

                entity.Property(e => e.CardDetails)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Seats__BookingId__6477ECF3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
