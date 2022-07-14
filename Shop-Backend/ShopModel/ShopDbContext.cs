using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop.Models
{
    public partial class ShopContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Profile> Profiles {get; set; } = null!;
        public DbSet<Inventory> Inventories {get; set; } = null!;
        public DbSet<LineItem> LineItems {get; set; } = null!;
        public DbSet<Order> Orders {get; set; } = null!;
        public DbSet<OrderToLineItem> OrderToLineItems { get; set; } = null!;
        public DbSet<Product> Products {get; set; } = null!;
        public DbSet<StoreFront> StoreFronts {get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts {get; set; } = null!;

        public ShopContext() : base()
        {}

        public ShopContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.HasKey(e => e.ProfileId)
                    .HasName("PK_ProfileID");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
                
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                    
                entity.Property(e => e.Age)
                    .HasMaxLength(3)
                    .HasColumnName("Age");
                
                entity.Property(e => e.Address)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Profiles )
                    .HasForeignKey<Profile>(d => d.UserId )
                    .HasConstraintName("FK_Profile_UserID");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.HasKey(e => e.ShoppingCartId)
                    .HasName("PK_ShoppingCartID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");
                
                entity.Property(e => e.ProductId)
                    .HasMaxLength(450)
                    .HasColumnName("ProductID");
                    
                entity.Property(e => e.StoreId)
                    .HasMaxLength(450)
                    .HasColumnName("StoreID");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(10);

                entity.HasOne(d => d.ApplicationUsers )
                    .WithMany(p => p.ShoppingCarts )
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_UserID");

                entity.HasOne(d => d.StoreFronts)
                    .WithMany(p => p.ShoppingCarts )
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_StoreID");

                entity.HasOne(d => d.Products )
                    .WithMany(p => p.ShoppingCarts )
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_ProductID");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.HasKey(e => e.InventoryId)
                    .HasName("PK_InventoryID");

                entity.Property(e => e.InventoryId)
                    .HasMaxLength(450)
                    .HasColumnName("InventoryID");
                
                entity.Property(e => e.ProductId)
                    .HasMaxLength(450)
                    .HasColumnName("ProductID");
                    
                entity.Property(e => e.ProductQuantity)
                    .HasMaxLength(10);

                entity.HasOne(d => d.Stores )
                    .WithMany(p => p.Inventories )
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_StoreID");

                entity.HasOne(d => d.Products )
                    .WithMany(p => p.Inventories )
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ProductID");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.ToTable("LineItem");
                
                entity.HasKey(e => e.LineItemId)
                    .HasName("PK_LineItemID");

                entity.Property(e => e.LineItemId)
                    .HasMaxLength(450)
                    .HasColumnName("LineItemID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(450)
                    .HasColumnName("ProductID");
                    
                entity.Property(e => e.ItemQuantity )
                    .HasMaxLength(10);

                /*entity.HasOne(d => d.OrdersToLineItems )
                    .WithMany(p => p. )
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreID"); */

                entity.HasOne(d => d.Products )
                    .WithMany(p => p.LineItems )
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItem__ProductID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasKey(e => e.OrderId)
                    .HasName("PK_OrderID");

                entity.Property(e => e.OrderId )
                    .HasMaxLength(450)
                    .HasColumnName("OrderID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.Property(e => e.StoreId)
                    .HasMaxLength(450)
                    .HasColumnName("StoreID");
                
                entity.Property(e => e.creationTime ).HasColumnType("smalldatetime");

                entity.HasOne(d => d.User )
                    .WithMany(p => p.Orders )
                    .HasForeignKey(d => d.UserId )
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_UserId");

                entity.HasOne(d => d.StoreFronts )
                    .WithMany(p => p.Orders )
                    .HasForeignKey(d => d.StoreId )
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_StoreId");
            });

            modelBuilder.Entity<OrderToLineItem>(entity =>
            {
                entity.ToTable("OrderToLineItem");

                entity.HasKey(e => e.OrderToLineItemId)
                    .HasName("PK_OrderToLineItemID");

                entity.Property(e => e.OrderToLineItemId)
                    .HasMaxLength(450)
                    .HasColumnName("OrderToLineItemID");
                
                entity.Property(e => e.OrderId)
                    .HasMaxLength(450)
                    .HasColumnName("OrderID");
                
                entity.Property(e => e.LineItemId)
                    .HasMaxLength(450)
                    .HasColumnName("LineItemID");

                entity.HasOne(d => d.Orders )
                    .WithMany(p => p.OrderToLineItems )
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderID");

                entity.HasOne(d => d.LineItems )
                    .WithMany(p => p.OrdersToLineItems )
                    .HasForeignKey(d => d.LineItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItemID");
            });

            modelBuilder.Entity<Product>( entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(e => e.ProductId)
                    .HasName("PK_ProductID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 8)");
                
                entity.Property(e => e.ProductDescription).HasMaxLength(450);

                entity.Property(e => e.ProductAgeRestriction).HasMaxLength(3);
            });

            modelBuilder.Entity<StoreFront>( entity =>
            {
                entity.ToTable("StoreFront");

                entity.HasKey(e => e.StoreId)
                    .HasName("PK_StoreID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.StoreName).HasMaxLength(50);

                entity.Property(e => e.StoreAddress).HasMaxLength(450);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

/*

                 
modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePictureUrl)
                    .IsUnicode(false)
                    .HasColumnName("ProfilePictureURL");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Profile__UserID__4D2A7347");
            });
            */