using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Data;

public partial class ProductManagementContext : DbContext
{
    public ProductManagementContext()
    {
    }

    public ProductManagementContext(DbContextOptions<ProductManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }
    public virtual DbSet<LoginMaster> LoginMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ProductManagement;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("order_details");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .HasColumnName("Model_No");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Specification)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.SubCategoryId).HasColumnName("sub_categoryId");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.ToTable("Product_stock");

            entity.Property(e => e.CurrentStock).HasColumnName("current_stock");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("Purchase_date");
            entity.Property(e => e.StockIn).HasColumnName("stock_in");
            entity.Property(e => e.StockOut).HasColumnName("stock_out");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseHeaderId, e.PurchaseOrderDetailsId });

            entity.ToTable("Purchase_order_details");

            entity.Property(e => e.PurchaseHeaderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Purchase_header_id");
            entity.Property(e => e.PurchaseOrderDetailsId).HasColumnName("purchase_order_Details_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");

            entity.HasOne(d => d.PurchaseHeader).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_order_details_Purchase_Order_Header");
        });

        modelBuilder.Entity<PurchaseOrderHeader>(entity =>
        {
            entity.HasKey(e => e.HeaderId);

            entity.ToTable("Purchase_Order_Header");

            entity.Property(e => e.HeaderId).HasColumnName("Header_id");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("Purchase_Date");
            entity.Property(e => e.TotalAmount).HasColumnName("Total_Amount");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.SalesHeaderId, e.SalesOrderDetailsId });

            entity.ToTable("Sales_order_details");

            entity.Property(e => e.SalesHeaderId).HasColumnName("Sales_header_id");
            entity.Property(e => e.SalesOrderDetailsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Sales_order_details_id");
            entity.Property(e => e.Cgst).HasColumnName("CGST");
            entity.Property(e => e.Gst).HasColumnName("GST");
            entity.Property(e => e.Igst).HasColumnName("IGST");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.HeaderId);

            entity.ToTable("Sales_order_Header");

            entity.Property(e => e.HeaderId).HasColumnName("Header_id");
            entity.Property(e => e.SalesDate)
                .HasColumnType("datetime")
                .HasColumnName("Sales_date");
            entity.Property(e => e.SalesType)
                .HasMaxLength(50)
                .HasColumnName("Sales_type");
            entity.Property(e => e.TotalAmount).HasColumnName("Total_Amount");
            entity.Property(e => e.TotalGst).HasColumnName("Total_GST");
            entity.Property(e => e.TotalIgst).HasColumnName("Total_IGST");
            entity.Property(e => e.TotalSgst).HasColumnName("Total_SGST");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.SubName)
                .HasMaxLength(50)
                .HasColumnName("Sub_name");
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("user_master");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone).HasMaxLength(50);
        });
        modelBuilder.Entity<LoginMaster>(entity =>
        {
            entity.HasKey(e => e.User_Name);

            entity.ToTable("LoginMaster");

            entity.Property(e => e.User_Name).HasColumnName("User_Name");
            entity.Property(e => e.Password).HasMaxLength(100).HasColumnName("Password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("Role");
            entity.Property(e => e.CreateOn).HasColumnType("datetime")
                .HasColumnName("CreateOn");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
