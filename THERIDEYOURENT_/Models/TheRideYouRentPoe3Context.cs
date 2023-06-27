using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace THERIDEYOURENT_.Models;

public partial class TheRideYouRentPoe3Context : DbContext
{
    public TheRideYouRentPoe3Context()
    {
    }

    public TheRideYouRentPoe3Context(DbContextOptions<TheRideYouRentPoe3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBodyType> CarBodyTypes { get; set; }

    public virtual DbSet<CarMake> CarMakes { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Inspector> Inspectors { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Return> Returns { get; set; }




    // DbSets and other configurations
    // ...



    // public virtual DbSet <CalculateDriverFine> CalculateDriverFine { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        if (!optionsBuilder.IsConfigured) {

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnectionStringDev"));
          //  optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnectionStringAZURE"));

        }

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Car__68A0342EE5066FCE");

            entity.ToTable("Car");

            entity.Property(e => e.Available)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CarNo)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.BodyType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.BodyTypeId)
                .HasConstraintName("FK__Car__BodyTypeId__3C69FB99");

            entity.HasOne(d => d.Make).WithMany(p => p.Cars)
                .HasForeignKey(d => d.MakeId)
                .HasConstraintName("FK__Car__MakeId__3B75D760");
        });

        modelBuilder.Entity<CarBodyType>(entity =>
        {
            entity.HasKey(e => e.BodyTypeId).HasName("PK__CarBodyT__3F42D9A1876CCB0A");

            entity.ToTable("CarBodyType");

            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarMake>(entity =>
        {
            entity.HasKey(e => e.MakeId).HasName("PK__CarMake__43646F51EB9F0069");

            entity.ToTable("CarMake");

            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD04C92598B4");

            entity.ToTable("Driver");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inspector>(entity =>
        {
            entity.HasKey(e => e.InspectorId).HasName("PK__Inspecto__5FECC3DD2306C9A6");

            entity.ToTable("Inspector");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__Login__4DDA2818FFCF0FF5");

            entity.ToTable("Login");

            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Logins)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Login__Username__4D94879B");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__Rental__97005943015A7A57");

            entity.ToTable("Rental");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.CarNoNavigation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CarNo)
                .HasConstraintName("FK__Rental__CarNo__4316F928");

            entity.HasOne(d => d.Driver).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Rental__DriverId__44FF419A");

            entity.HasOne(d => d.InspectorNoNavigation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.InspectorNo)
                .HasConstraintName("FK__Rental__Inspecto__440B1D61");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__Return__F445E9A8CCDA47F4");

            entity.ToTable("Return");

            entity.Property(e => e.ReturnDate).HasColumnType("date");

            entity.HasOne(d => d.CarNoNavigation).WithMany(p => p.Returns)
                .HasForeignKey(d => d.CarNo)
                .HasConstraintName("FK__Return__CarNo__47DBAE45");

            entity.HasOne(d => d.Driver).WithMany(p => p.Returns)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Return__DriverId__49C3F6B7");

            entity.HasOne(d => d.InspectorNoNavigation).WithMany(p => p.Returns)
                .HasForeignKey(d => d.InspectorNo)
                .HasConstraintName("FK__Return__Inspecto__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);



    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
