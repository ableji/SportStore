using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Data
{
    public partial class ApplicationDBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //tables name and schema
            modelBuilder.Entity<Product>().ToTable("Products", "SS");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories", "SS");
            modelBuilder.Entity<CartItem>().ToTable("CartsItems", "SS");
            modelBuilder.Entity<Order>().ToTable("Orders", "SS");
            modelBuilder.Entity<Image>().ToTable("Images","SS");
            modelBuilder.Entity<Province>().ToTable("Provinces", "SS");
            modelBuilder.Entity<City>().ToTable("Cities", "SS");
            modelBuilder.Entity<Message>().ToTable("Messages", "SS");



            #region Product

            //one to many Product ---> Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCagegoryID)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);

            #endregion

            #region ProductCategory


            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Childs)
                .WithOne(pc => pc.Parent)
                .HasForeignKey(pc => pc.ParentID);
            //onDelete Error:
            //Introducing FOREIGN KEY constraint 'FK_ProductCategories_ProductCategories_ParentID' on table 'ProductCategories' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
            
            #endregion

            #region CartItem

    

            //ont to many cartitem --> order
            modelBuilder.Entity<CartItem>()
           .HasOne(CI => CI.Order)
           .WithMany(o => o.CartItems)
           .HasForeignKey(CI => CI.OrderID)
           .IsRequired()
           .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            //one to many cartitem --> product
            modelBuilder.Entity<CartItem>()
                .HasOne(i => i.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductID)
                .IsRequired();


            #endregion

            #region Order



            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(sh => sh.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            #endregion

            #region City



            //ont to many city --> province
            modelBuilder.Entity<City>()
           .HasOne(c => c.Province)
           .WithMany(p => p.Cities)
           .HasForeignKey(c => c.ProvinceID)
           .IsRequired()
           .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);


            #endregion

            #region Province




            #endregion


            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
            modelBuilder.Ignore<AppUser>();



        }

        
    }
}
