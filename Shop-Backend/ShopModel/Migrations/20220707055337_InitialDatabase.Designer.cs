﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Models;

#nullable disable

namespace ShopModel.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20220707055337_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Shop.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Authority")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Shop.Models.Inventory", b =>
                {
                    b.Property<string>("InventoryId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("InventoryID");

                    b.Property<string>("ProductId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProductID");

                    b.Property<int>("ProductQuantity")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("StoreId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("InventoryId")
                        .HasName("PK_InventoryID");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.ToTable("Inventory", (string)null);
                });

            modelBuilder.Entity("Shop.Models.LineItem", b =>
                {
                    b.Property<string>("LineItemId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("LineItemID");

                    b.Property<int>("ItemQuantity")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("ProductId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProductID");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("LineItemId")
                        .HasName("PK_LineItemID");

                    b.HasIndex("ProductId");

                    b.ToTable("LineItem", (string)null);
                });

            modelBuilder.Entity("Shop.Models.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OrderID");

                    b.Property<string>("StoreId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("StoreID");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserID");

                    b.Property<DateTime>("creationTime")
                        .HasColumnType("smalldatetime");

                    b.HasKey("OrderId")
                        .HasName("PK_OrderID");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Shop.Models.OrderToLineItem", b =>
                {
                    b.Property<string>("OrderToLineItemId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OrderToLineItemID");

                    b.Property<string>("LineItemId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("LineItemID");

                    b.Property<string>("OrderId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OrderID");

                    b.HasKey("OrderToLineItemId")
                        .HasName("PK_OrderToLineItemID");

                    b.HasIndex("LineItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderToLineItem", (string)null);
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProductID");

                    b.Property<int>("ProductAgeRestriction")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,8)");

                    b.HasKey("ProductId")
                        .HasName("PK_ProductID");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Shop.Models.Profile", b =>
                {
                    b.Property<string>("ProfileId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProfileID");

                    b.Property<string>("Address")
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)");

                    b.Property<int>("Age")
                        .HasMaxLength(3)
                        .HasColumnType("int")
                        .HasColumnName("Age");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProfileId")
                        .HasName("PK_ProfileID");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Profile", (string)null);
                });

            modelBuilder.Entity("Shop.Models.ShoppingCart", b =>
                {
                    b.Property<string>("ShoppingCartId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("StoreId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("StoreID");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserID");

                    b.HasKey("ShoppingCartId")
                        .HasName("PK_ShoppingCartID");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCart", (string)null);
                });

            modelBuilder.Entity("Shop.Models.StoreFront", b =>
                {
                    b.Property<string>("StoreId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("StoreID");

                    b.Property<string>("StoreAddress")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StoreName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StoreId")
                        .HasName("PK_StoreID");

                    b.ToTable("StoreFront", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Shop.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Shop.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Shop.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shop.Models.Inventory", b =>
                {
                    b.HasOne("Shop.Models.Product", "Products")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Inventory_ProductID");

                    b.HasOne("Shop.Models.StoreFront", "Stores")
                        .WithMany("Inventories")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Inventory_StoreID");

                    b.Navigation("Products");

                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Shop.Models.LineItem", b =>
                {
                    b.HasOne("Shop.Models.Product", "Products")
                        .WithMany("LineItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_LineItem__ProductID");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Models.Order", b =>
                {
                    b.HasOne("Shop.Models.StoreFront", "StoreFronts")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Order_StoreId");

                    b.HasOne("Shop.Models.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Order_UserId");

                    b.Navigation("StoreFronts");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Models.OrderToLineItem", b =>
                {
                    b.HasOne("Shop.Models.LineItem", "LineItems")
                        .WithMany("OrdersToLineItems")
                        .HasForeignKey("LineItemId")
                        .HasConstraintName("FK_LineItemID");

                    b.HasOne("Shop.Models.Order", "Orders")
                        .WithMany("OrderToLineItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderID");

                    b.Navigation("LineItems");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Shop.Models.Profile", b =>
                {
                    b.HasOne("Shop.Models.ApplicationUser", "User")
                        .WithOne("Profiles")
                        .HasForeignKey("Shop.Models.Profile", "UserId")
                        .HasConstraintName("FK_Profile_UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Models.ShoppingCart", b =>
                {
                    b.HasOne("Shop.Models.Product", "Products")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_ShoppingCart_ProductID");

                    b.HasOne("Shop.Models.StoreFront", "StoreFronts")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FK_ShoppingCart_StoreID");

                    b.HasOne("Shop.Models.ApplicationUser", "ApplicationUsers")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_ShoppingCart_UserID");

                    b.Navigation("ApplicationUsers");

                    b.Navigation("Products");

                    b.Navigation("StoreFronts");
                });

            modelBuilder.Entity("Shop.Models.ApplicationUser", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Profiles");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("Shop.Models.LineItem", b =>
                {
                    b.Navigation("OrdersToLineItems");
                });

            modelBuilder.Entity("Shop.Models.Order", b =>
                {
                    b.Navigation("OrderToLineItems");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("LineItems");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("Shop.Models.StoreFront", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("Orders");

                    b.Navigation("ShoppingCarts");
                });
#pragma warning restore 612, 618
        }
    }
}
