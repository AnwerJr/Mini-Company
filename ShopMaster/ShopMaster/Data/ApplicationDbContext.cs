using Microsoft.EntityFrameworkCore;
using ShopMaster.Models;
using System;

namespace ShopMaster.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================================
            // Category Configuration
            // ============================================
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.NameEn);
                entity.HasIndex(e => e.IsActive);
            });

            // ============================================
            // Product Configuration
            // ============================================
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.SKU).IsUnique();
                entity.HasIndex(e => e.CategoryId);
                entity.HasIndex(e => e.IsActive);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // Customer Configuration
            // ============================================
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Phone);
            });

            // ============================================
            // Order Configuration
            // ============================================
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.HasIndex(e => e.CustomerId);
                entity.HasIndex(e => e.OrderDate);
                entity.HasIndex(e => e.Status);

                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // OrderItem Configuration
            // ============================================
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // Shipment Configuration
            // ============================================
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TrackingNumber).IsUnique();
                entity.HasIndex(e => e.Status);

                entity.HasOne(s => s.Order)
                    .WithOne(o => o.Shipment)
                    .HasForeignKey<Shipment>(s => s.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================================
            // User Configuration
            // ============================================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.IsActive);
            });

            // ============================================
            // SystemSetting Configuration
            // ============================================
            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Key).IsUnique();
            });

            // ============================================
            // Seed Data
            // ============================================
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // ============================================
            // Seed Categories
            // ============================================
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    NameAr = "إلكترونيات",
                    NameEn = "Electronics",
                    Description = "أجهزة إلكترونية ومعدات تقنية",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Category
                {
                    Id = 2,
                    NameAr = "ملابس",
                    NameEn = "Clothing",
                    Description = "ملابس رجالية ونسائية وأطفال",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Category
                {
                    Id = 3,
                    NameAr = "كتب",
                    NameEn = "Books",
                    Description = "كتب ومراجع علمية وأدبية",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            );

            // ============================================
            // Seed Default Admin User
            // ============================================
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "المسؤول الرئيسي",
                    Email = "admin@shopmaster.com",
                    Username = "admin",
                    Password = "Admin@123", // يجب تشفيرها في الإنتاج
                    Phone = "0123456789",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = 2,
                    FullName = "مدير المبيعات",
                    Email = "manager@shopmaster.com",
                    Username = "manager",
                    Password = "Manager@123",
                    Phone = "0123456780",
                    Role = UserRole.Manager,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            );

            // ============================================
            // Seed System Settings
            // ============================================
            modelBuilder.Entity<SystemSetting>().HasData(
                new SystemSetting
                {
                    Id = 1,
                    Key = "StoreName",
                    Value = "ShopMaster",
                    Description = "اسم المتجر",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 2,
                    Key = "StoreEmail",
                    Value = "info@shopmaster.com",
                    Description = "البريد الإلكتروني للمتجر",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 3,
                    Key = "StorePhone",
                    Value = "0123456789",
                    Description = "رقم الهاتف",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 4,
                    Key = "StoreAddress",
                    Value = "القاهرة، مصر",
                    Description = "عنوان المتجر",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 5,
                    Key = "Currency",
                    Value = "ج.م",
                    Description = "رمز العملة",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 6,
                    Key = "TaxRate",
                    Value = "14",
                    Description = "نسبة الضريبة المضافة (%)",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 7,
                    Key = "DefaultShippingCost",
                    Value = "50",
                    Description = "تكلفة الشحن الافتراضية",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 8,
                    Key = "MinOrderAmount",
                    Value = "100",
                    Description = "الحد الأدنى لقيمة الطلب",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 9,
                    Key = "FreeShippingThreshold",
                    Value = "500",
                    Description = "قيمة الطلب للشحن المجاني",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 10,
                    Key = "LowStockAlert",
                    Value = "10",
                    Description = "حد التنبيه لانخفاض المخزون",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 11,
                    Key = "WorkingHours",
                    Value = "السبت - الخميس: 9 صباحاً - 9 مساءً",
                    Description = "ساعات العمل",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 12,
                    Key = "SupportEmail",
                    Value = "support@shopmaster.com",
                    Description = "بريد الدعم الفني",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 13,
                    Key = "ReturnPolicy",
                    Value = "14",
                    Description = "مدة سياسة الإرجاع (بالأيام)",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 14,
                    Key = "AllowGuestCheckout",
                    Value = "true",
                    Description = "السماح بالشراء بدون تسجيل",
                    UpdatedAt = new DateTime(2024, 1, 1)
                },
                new SystemSetting
                {
                    Id = 15,
                    Key = "MaintenanceMode",
                    Value = "false",
                    Description = "وضع الصيانة",
                    UpdatedAt = new DateTime(2024, 1, 1)
                }
            );
        }
    
        // Seed Data
        //modelBuilder.Entity<Category>().HasData(
        //    new Category { Id = 1, NameAr = "إلكترونيات", NameEn = "Electronics", Description = "أجهزة إلكترونية" },
        //    new Category { Id = 2, NameAr = "ملابس", NameEn = "Clothing", Description = "ملابس رجالية ونسائية" },
        //    new Category { Id = 3, NameAr = "كتب", NameEn = "Books", Description = "كتب ومراجع" }
        //);

        //modelBuilder.Entity<Product>().HasData(
        //    new Product
        //    {
        //        Id = 1,
        //        NameAr = "لابتوب HP",
        //        NameEn = "HP Laptop",
        //        Price = 15000,
        //        StockQuantity = 10,
        //        CategoryId = 1,
        //        SKU = "LAP-HP-001",
        //        DescriptionAr = "لابتوب HP بمعالج Core i7",
        //        DescriptionEn = "Default description",
        //        ImageUrl = "default-product.png",
        //        IsActive = true,
        //        IsFeatured = false,
        //        CreatedAt = DateTime.Now
        //    },
        //    new Product
        //    {
        //        Id = 2,
        //        NameAr = "قميص رجالي",
        //        NameEn = "Men's Shirt",
        //        Price = 250,
        //        StockQuantity = 50,
        //        CategoryId = 2,
        //        SKU = "SHIRT-001",
        //        DescriptionAr = "قميص رجالي قطن 100%",
        //        DescriptionEn = "Default description",
        //        ImageUrl = "default-product.png",
        //        IsActive = true,
        //        IsFeatured = false,
        //        CreatedAt = DateTime.Now
        //    }
        //);
    }
}
