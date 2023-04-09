using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalonNamjestaja.Data;

public partial class FurnitureDbContext : DbContext
{
    public FurnitureDbContext()
    {
    }

    public FurnitureDbContext(DbContextOptions<FurnitureDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BonusCard> BonusCards { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-3C5LH42\\SQLEXPRESS;Initial Catalog=Furniture;Integrated Security=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address", "Furniture");

            entity.Property(e => e.AddressId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("AddressID");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Number)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Zip).HasColumnType("numeric(10, 0)");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.ToTable("Bill", "Furniture", tb => tb.HasTrigger("UniqueCheck"));

            entity.HasIndex(e => e.OrderId, "UQ__Bill__C3905BAEAB1222A6").IsUnique();

            entity.Property(e => e.BillId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("BillID");
            entity.Property(e => e.Amount).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.OrderId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("OrderID");
            entity.Property(e => e.Publisher)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Vat)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("VAT");
        });

        modelBuilder.Entity<BonusCard>(entity =>
        {
            entity.ToTable("BonusCard", "Furniture");

            entity.Property(e => e.BonusCardId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("BonusCardID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("CustomerID");
            entity.Property(e => e.Number).HasColumnType("numeric(20, 0)");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.BonusCards)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BonusCard_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer", "Furniture");

            entity.Property(e => e.CustomerId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("CustomerID");
            entity.Property(e => e.AddressId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("AddressID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("UserID");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Address");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_UserType");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders", "Furniture", tb => tb.HasTrigger("Discount"));

            entity.Property(e => e.OrderId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("CustomerID");
            entity.Property(e => e.DeliveryDate).HasColumnType("date");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice)
                .HasColumnType("numeric(10, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customer");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderItemId, e.OrderId });

            entity.ToTable("OrderItem", "Furniture");

            entity.Property(e => e.OrderItemId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.ProductId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("ProductID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("numeric(8, 0)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product", "Furniture");

            entity.Property(e => e.ProductId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("ProductID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("CategoryID");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(10, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ProductCategory", "Furniture");

            entity.Property(e => e.CategoryId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("CategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserType", "Furniture");

            entity.Property(e => e.UserId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("UserID");
            entity.Property(e => e.UserRole)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
