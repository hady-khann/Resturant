﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Resturant.DataAccess.Context;

namespace Resturant.DataAccess.Migrations
{
    [DbContext(typeof(ResturantContext))]
    [Migration("20211215112017_SyncMigration")]
    partial class SyncMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Persian_100_CI_AI")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Resturant.DBModels.Entities.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity("Resturant.DBModels.Entities.FoodType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("FoodType");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.Resturant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity("Resturant.DBModels.Entities.ResturantsFood", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity("Resturant.DBModels.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasMaxLength(44)
                        .HasColumnType("nvarchar(44)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Wallet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.UserOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity("Resturant.DBModels.Entities.ViwResturant", b =>
                {
                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

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

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.ToView("Viw_Resturants");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.ViwResturantFood", b =>
                {
                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FoodID");

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<Guid>("IdFood")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id_Food");

                    b.Property<Guid>("IdResFood")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id_ResFood");

                    b.Property<Guid>("IdResturant")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id_Resturant");

                    b.Property<Guid>("IdType")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id_Type");

                    b.Property<string>("PicFood")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Pic_Food");

                    b.Property<string>("PicResturant")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Pic_Resturant");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<Guid>("ResturantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ResturantID");

                    b.Property<string>("ResturantName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ResturantType")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TypeID");

                    b.ToView("Viw_ResturantFoods");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.ViwUsersInfo", b =>
                {
                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasMaxLength(44)
                        .HasColumnType("nvarchar(44)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Wallet")
                        .HasColumnType("int");

                    b.ToView("Viw_UsersInfo");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.Food", b =>
                {
                    b.HasOne("Resturant.DBModels.Entities.FoodType", "Type")
                        .WithMany("Foods")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_Foods_FoodType")
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.Resturant", b =>
                {
                    b.HasOne("Resturant.DBModels.Entities.FoodType", "ResturantTypeNavigation")
                        .WithMany("Resturants")
                        .HasForeignKey("ResturantType")
                        .HasConstraintName("FK_Resturant_FoodType")
                        .IsRequired();

                    b.Navigation("ResturantTypeNavigation");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.ResturantsFood", b =>
                {
                    b.HasOne("Resturant.DBModels.Entities.Food", "Resturant")
                        .WithMany("ResturantsFoods")
                        .HasForeignKey("ResturantId")
                        .HasConstraintName("FK_ResturantsFoods_Foods")
                        .IsRequired();

                    b.HasOne("Resturant.DBModels.Entities.Resturant", "ResturantNavigation")
                        .WithMany("ResturantsFoods")
                        .HasForeignKey("ResturantId")
                        .HasConstraintName("FK_ResturantsFoods_Resturant")
                        .IsRequired();

                    b.Navigation("Resturant");

                    b.Navigation("ResturantNavigation");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.User", b =>
                {
                    b.HasOne("Resturant.DBModels.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_User_Roles")
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.UserOrder", b =>
                {
                    b.HasOne("Resturant.DBModels.Entities.Food", "Food")
                        .WithMany("UserOrders")
                        .HasForeignKey("FoodId")
                        .HasConstraintName("FK_UserOrders_Foods")
                        .IsRequired();

                    b.HasOne("Resturant.DBModels.Entities.Resturant", "Resturant")
                        .WithMany("UserOrders")
                        .HasForeignKey("ResturantId")
                        .HasConstraintName("FK_UserOrders_Resturant")
                        .IsRequired();

                    b.HasOne("Resturant.DBModels.Entities.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserOrders_User")
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Resturant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.Food", b =>
                {
                    b.Navigation("ResturantsFoods");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.FoodType", b =>
                {
                    b.Navigation("Foods");

                    b.Navigation("Resturants");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.Resturant", b =>
                {
                    b.Navigation("ResturantsFoods");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Resturant.DBModels.Entities.User", b =>
                {
                    b.Navigation("UserOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
