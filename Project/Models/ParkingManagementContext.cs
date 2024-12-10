using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Project.Models;

public partial class ParkingManagementContext : DbContext
{
    public ParkingManagementContext()
    {
    }

    public ParkingManagementContext(DbContextOptions<ParkingManagementContext> options)
        : base(options)
    {
    }
    public static ParkingManagementContext Ins { get; } = new ParkingManagementContext();
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<ParkingLot> ParkingLots { get; set; }

    public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    private String GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3214EC2775100A9C");

            entity.HasIndex(e => e.Username, "UQ__Accounts__F3DBC5727860EB6D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<ParkingLot>(entity =>
        {
            entity.HasKey(e => e.LotId).HasName("PK__ParkingL__4160EF4DDC67DDBF");

            entity.ToTable("ParkingLot");

            entity.Property(e => e.LotId).HasColumnName("LotID");
            entity.Property(e => e.Amount)
                .HasDefaultValue(0)
                .HasColumnName("amount");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LotSector)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Employee).WithMany(p => p.ParkingLots)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ParkingLo__Emplo__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.ParkingLots)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ParkingLo__UserI__403A8C7D");
        });

        modelBuilder.Entity<PersonalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personal__3214EC279F3A8C3F");

            entity.ToTable("PersonalInfo");

            entity.HasIndex(e => e.Email, "UQ__Personal__AB6E6164500C3CB2").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0)
                .HasColumnName("balance");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(20)
                .HasColumnName("licensePlate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParkingDate)
                .HasColumnType("datetime")
                .HasColumnName("parkingDate");
            entity.Property(e => e.RetrievalDate)
                .HasColumnType("datetime")
                .HasColumnName("retrievalDate");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PersonalInfo)
                .HasForeignKey<PersonalInfo>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonalInfo__ID__3C69FB99");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC6274DFB771B");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiryDate");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("purchaseDate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Type).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__TypeID__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__UserID__45F365D3");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TicketTy__516F039583FF383B");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("typeName");
            entity.Property(e => e.ValidityDays).HasColumnName("validityDays");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
