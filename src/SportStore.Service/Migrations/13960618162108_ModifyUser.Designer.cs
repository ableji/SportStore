using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SportStore.Data;

namespace SportStore.Service.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("13960618162108_ModifyUser")]
    partial class ModifyUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportStore.Model.Domain.CartItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("InsertedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("OrderID");

                    b.Property<int>("ProductID");

                    b.Property<int>("Quantity");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartsItems","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ProvinceID");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceID");

                    b.ToTable("Cities","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("IMG");

                    b.HasKey("Id");

                    b.ToTable("Images","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("InsertedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("Shiped");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.ToTable("Orders","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ImageId");

                    b.Property<DateTime>("InsertedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ProductCagegoryID");

                    b.Property<string>("ProductCompany");

                    b.Property<string>("ProductDescription");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("ProductPrice");

                    b.Property<int>("Quantity");

                    b.Property<string>("ShortDescription");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("ID");

                    b.HasIndex("ProductCagegoryID");

                    b.ToTable("Products","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.ProductCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("InsertedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentID");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("ID");

                    b.HasIndex("ParentID");

                    b.ToTable("ProductCategories","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Provinces","SS");
                });

            modelBuilder.Entity("SportStore.Model.Domain.CartItem", b =>
                {
                    b.HasOne("SportStore.Model.Domain.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportStore.Model.Domain.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportStore.Model.Domain.City", b =>
                {
                    b.HasOne("SportStore.Model.Domain.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportStore.Model.Domain.Product", b =>
                {
                    b.HasOne("SportStore.Model.Domain.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCagegoryID")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("SportStore.Model.Domain.ProductCategory", b =>
                {
                    b.HasOne("SportStore.Model.Domain.ProductCategory", "Parent")
                        .WithMany("Childs")
                        .HasForeignKey("ParentID");
                });
        }
    }
}
