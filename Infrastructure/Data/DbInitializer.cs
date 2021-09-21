using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();

            if (context.Category.Any()) //look for any categories.
            {
                return; //DB has been seeded
            }

            var category = new List<Category>
            {
                new Category{Name="Soup", DisplayOrder=1},
                new Category{Name="Salad", DisplayOrder=2},
                new Category{Name="Entrees", DisplayOrder=3},
                new Category{Name="Dessert", DisplayOrder=5},
                new Category{Name="Beverages", DisplayOrder=4}
            };

            foreach(Category c in category)
            {
                context.Category.Add(c);
            }
            context.SaveChanges();

            var foodtype = new List<FoodType>
            {
                new FoodType{Name="Beef"},
                new FoodType{Name="Chicken"},
                new FoodType{Name="Veggie"},
                new FoodType{Name="Sugar Free"},
                new FoodType{Name="SeaFood"},
                new FoodType{Name="Dairy Free"}
            };

            foreach(FoodType f in foodtype)
            {
                context.FoodType.Add(f);
            }

            context.SaveChanges();

        }
    }
}
