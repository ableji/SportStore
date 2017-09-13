using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Model.Domain;
using System.Linq;


namespace SportStore.Data
{
    public class SeedData
    {
        //put some data in ProductsTable when its empty 
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDBContext contex =
                app.ApplicationServices.GetRequiredService<ApplicationDBContext>();

            if (!contex.Products.Any())
            {
                contex.Products.AddRange(

                #region initialize

                      new Product
                      {
                          ProductName = "Boxing Gloves",
                          ProductPrice = 20000,
                          ProductCategory = new ProductCategory { Name = "Boxing" },
                          Quantity=50

                      },

                      new Product
                      {
                          ProductName = "ball",
                          ProductPrice = 64000,
                          ProductCategory = new ProductCategory { Name = "soccer" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "basketBallShoes",
                          ProductPrice = 150000,
                          ProductCategory = new ProductCategory { Name = "BaskerBall" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "Tshit",
                          ProductPrice = 2000,
                          ProductCategory = new ProductCategory { Name = "Training" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "Board",
                          ProductPrice = 40000,
                          ProductCategory = new ProductCategory { Name = "ski" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "Hat",
                          ProductPrice = 32000,
                          ProductCategory = new ProductCategory { Name = "FootBall" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "Tshirt",
                          ProductPrice = 5000,
                          ProductCategory = new ProductCategory { Name = "General" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "SwimingGlass",
                          ProductPrice = 800000,
                          ProductCategory = new ProductCategory { Name = "Swim" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "Runnig Shoes",
                          ProductPrice = 350000,
                          ProductCategory = new ProductCategory { Name = "Running" },
                          Quantity = 50

                      },

                      new Product
                      {
                          ProductName = "Tocket",
                          ProductPrice = 950000,
                          ProductCategory = new ProductCategory { Name = "Tenis" },
                          Quantity = 50

                      }

                      #endregion


                );

                contex.SaveChanges();

            }
        }
    }
}
