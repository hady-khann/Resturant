﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Resturant.Core.Models;

namespace Resturant.Core.Migrations
{
    [DbContext(typeof(ResturantContext))]
    [Migration("20211119212405_20221119")]
    partial class _20221119
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Persian_100_CI_AI")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Resturant.Core.Models.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TypeID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TypeId" }, "IX_Foods_TypeID");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Resturant.Core.Models.FoodType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("FoodType");
                });

            modelBuilder.Entity("Resturant.Core.Models.Resturant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("ResturantName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ResturantType")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ResturantType" }, "IX_Resturant_ResturantType");

                    b.ToTable("Resturant");
                });

            modelBuilder.Entity("Resturant.Core.Models.ResturantsFood", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FoodID");

                    b.Property<Guid>("ResturantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ResturantID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ResturantId" }, "IX_ResturantsFoods_ResturantID");

                    b.ToTable("ResturantsFoods");
                });

            modelBuilder.Entity("Resturant.Core.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ParentID");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Resturant.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleID");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Wallet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoleId" }, "IX_User_RoleID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Resturant.Core.Models.UserOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FoodCount")
                        .HasColumnType("int");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FoodID");

                    b.Property<Guid>("ResturantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ResturantID");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "FoodId" }, "IX_UserOrders_FoodID");

                    b.HasIndex(new[] { "ResturantId" }, "IX_UserOrders_ResturantID");

                    b.HasIndex(new[] { "UserId" }, "IX_UserOrders_UserId");

                    b.ToTable("UserOrders");
                });

            modelBuilder.Entity("Resturant.Core.Models.Food", b =>
                {
                    b.HasOne("Resturant.Core.Models.FoodType", "Type")
                        .WithMany("Foods")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_Foods_FoodType")
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Resturant.Core.Models.Resturant", b =>
                {
                    b.HasOne("Resturant.Core.Models.FoodType", "ResturantTypeNavigation")
                        .WithMany("Resturants")
                        .HasForeignKey("ResturantType")
                        .HasConstraintName("FK_Resturant_FoodType")
                        .IsRequired();

                    b.Navigation("ResturantTypeNavigation");
                });

            modelBuilder.Entity("Resturant.Core.Models.ResturantsFood", b =>
                {
                    b.HasOne("Resturant.Core.Models.Food", "Resturant")
                        .WithMany("ResturantsFoods")
                        .HasForeignKey("ResturantId")
                        .HasConstraintName("FK_ResturantsFoods_Foods")
                        .IsRequired();

                    b.HasOne("Resturant.Core.Models.Resturant", "ResturantNavigation")
                        .WithMany("ResturantsFoods")
                        .HasForeignKey("ResturantId")
                        .HasConstraintName("FK_ResturantsFoods_Resturant")
                        .IsRequired();

                    b.Navigation("Resturant");

                    b.Navigation("ResturantNavigation");
                });

            modelBuilder.Entity("Resturant.Core.Models.User", b =>
                {
                    b.HasOne("Resturant.Core.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_User_Roles")
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Resturant.Core.Models.UserOrder", b =>
                {
                    b.HasOne("Resturant.Core.Models.Food", "Food")
                        .WithMany("UserOrders")
                        .HasForeignKey("FoodId")
                        .HasConstraintName("FK_UserOrders_Foods")
                        .IsRequired();

                    b.HasOne("Resturant.Core.Models.Resturant", "Resturant")
                        .WithMany("UserOrders")
                        .HasForeignKey("ResturantId")
                        .HasConstraintName("FK_UserOrders_Resturant")
                        .IsRequired();

                    b.HasOne("Resturant.Core.Models.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserOrders_User")
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Resturant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Resturant.Core.Models.Food", b =>
                {
                    b.Navigation("ResturantsFoods");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("Resturant.Core.Models.FoodType", b =>
                {
                    b.Navigation("Foods");

                    b.Navigation("Resturants");
                });

            modelBuilder.Entity("Resturant.Core.Models.Resturant", b =>
                {
                    b.Navigation("ResturantsFoods");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("Resturant.Core.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Resturant.Core.Models.User", b =>
                {
                    b.Navigation("UserOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
