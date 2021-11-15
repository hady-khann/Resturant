using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Resturant.Core.Models
{
    public partial class ResturantContext : DbContext
    {
        public ResturantContext()
        {
        }

        public ResturantContext(DbContextOptions<ResturantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodType> FoodTypes { get; set; }
        public virtual DbSet<Resturant> Resturants { get; set; }
        public virtual DbSet<ResturantsFood> ResturantsFoods { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserOrder> UserOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Resturant;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Persian_100_CI_AI");

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasIndex(e => e.TypeId, "IX_Foods_TypeID");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.FoodName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Foods_FoodType");
            });

            modelBuilder.Entity<FoodType>(entity =>
            {
                entity.ToTable("FoodType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Resturant>(entity =>
            {
                entity.ToTable("Resturant");

                entity.HasIndex(e => e.ResturantType, "IX_Resturant_ResturantType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ResturantName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ResturantTypeNavigation)
                    .WithMany(p => p.Resturants)
                    .HasForeignKey(d => d.ResturantType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resturant_FoodType");
            });

            modelBuilder.Entity<ResturantsFood>(entity =>
            {
                entity.HasIndex(e => e.ResturantId, "IX_ResturantsFoods_ResturantID");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.ResturantId).HasColumnName("ResturantID");

                entity.HasOne(d => d.Resturant)
                    .WithMany(p => p.ResturantsFoods)
                    .HasForeignKey(d => d.ResturantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResturantsFoods_Foods");

                entity.HasOne(d => d.ResturantNavigation)
                    .WithMany(p => p.ResturantsFoods)
                    .HasForeignKey(d => d.ResturantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResturantsFoods_Resturant");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.RoleId, "IX_User_RoleID");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Roles");
            });

            modelBuilder.Entity<UserOrder>(entity =>
            {
                entity.HasIndex(e => e.FoodId, "IX_UserOrders_FoodID");

                entity.HasIndex(e => e.ResturantId, "IX_UserOrders_ResturantID");

                entity.HasIndex(e => e.UserId, "IX_UserOrders_UserId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.ResturantId).HasColumnName("ResturantID");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOrders_Foods");

                entity.HasOne(d => d.Resturant)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.ResturantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOrders_Resturant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOrders_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
