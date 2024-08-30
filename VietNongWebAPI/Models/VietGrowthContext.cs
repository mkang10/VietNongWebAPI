using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VietNongWebAPI.Models;

public partial class VietGrowthContext : DbContext
{
    public VietGrowthContext()
    {
    }

    public VietGrowthContext(DbContextOptions<VietGrowthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Crop> Crops { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Performance> Performances { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Technology> Technologies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Weather> Weathers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Crop>(entity =>
        {
            entity.HasKey(e => e.CropId).HasName("PK__Crops__923561352D14BC93");

            entity.Property(e => e.CropId).HasColumnName("CropID");
            entity.Property(e => e.CropName).HasMaxLength(100);
            entity.Property(e => e.FarmId).HasColumnName("FarmID");

            entity.HasOne(d => d.Farm).WithMany(p => p.Crops)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__Crops__FarmID__3E52440B");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.FarmId).HasName("PK__Farms__ED7BBA994A67351D");

            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.FarmName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Farms)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Farms__UserID__3B75D760");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.GuideId).HasName("PK__Guides__E77EE03EA834F04A");

            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__954EBDD32EB0606D");

            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF918FADF6");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__4CA06362");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__OrderHis__4D7B4ADD617CC374");

            entity.ToTable("OrderHistory");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderHist__Order__5441852A");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A1707C0C28");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__4F7CD00D");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderItem__Produ__5070F446");
        });

        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasKey(e => e.PerformanceId).HasName("PK__Performa__F9606DE1DF9875AE");

            entity.ToTable("Performance");

            entity.Property(e => e.PerformanceId).HasColumnName("PerformanceID");
            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.Yield).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Farm).WithMany(p => p.Performances)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__Performan__FarmI__440B1D61");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED318F724D");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.SellerId).HasColumnName("SellerID");

            entity.HasOne(d => d.Seller).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Products__Seller__47DBAE45");
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechId).HasName("PK__Technolo__8AFFB89FF0EB59C6");

            entity.ToTable("Technology");

            entity.Property(e => e.TechId).HasColumnName("TechID");
            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.TechName).HasMaxLength(100);

            entity.HasOne(d => d.Farm).WithMany(p => p.Technologies)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__Technolog__FarmI__412EB0B6");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC57D3FC91");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053422B78D4C").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserType).HasMaxLength(50);
        });

        modelBuilder.Entity<Weather>(entity =>
        {
            entity.HasKey(e => e.WeatherId).HasName("PK__Weather__0BF97BD5C9408054");

            entity.ToTable("Weather");

            entity.Property(e => e.WeatherId).HasColumnName("WeatherID");
            entity.Property(e => e.Humidity).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
