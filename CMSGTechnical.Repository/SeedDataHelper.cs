using System.ComponentModel;
using CMSGTechnical.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CMSGTechnical.Repository;

internal static class SeedDataHelper
{
    public static ModelBuilder SeedData(this ModelBuilder builder)
    {
        builder.SeedBasket();
        builder.SeedMenu();

        return builder;
    }

    private static void SeedBasket(this ModelBuilder builder)
    {
        builder.Entity<Basket>().HasData(new Basket() { Id = 1 });
    }

    private static void SeedMenu(this ModelBuilder builder)
    {
        var id = 1;
        builder
            .Entity<MenuItem>()
            .HasData(
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Margherita Pizza",
                    Description = "Classic pizza with fresh tomatoes, mozzarella cheese, and basil.",
                    Category = "Pizza",
                    Quantity = 1,
                    Price = 12.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Caesar Salad",
                    Description = "Crisp romaine lettuce with Caesar dressing, croutons, and parmesan cheese.",
                    Category = "Salad",
                    Quantity = 1,
                    Price = 8.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Grilled Salmon",
                    Description = "Grilled salmon fillet served with a side of roasted vegetables.",
                    Category = "Main Course",
                    Quantity = 1,
                    Price = 18.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Spaghetti Carbonara",
                    Description = "Pasta in a creamy sauce with pancetta, parmesan cheese, and black pepper.",
                    Category = "Pasta",
                    Quantity = 1,
                    Price = 14.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Vegetable Stir Fry",
                    Description = "Mixed vegetables stir-fried in a savory sauce, served with steamed rice.",
                    Category = "Vegetarian",
                    Quantity = 1,
                    Price = 11.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Chicken Parmesan",
                    Description = "Breaded chicken breast topped with marinara sauce and mozzarella cheese, served with pasta.",
                    Category = "Main Course",
                    Quantity = 1,
                    Price = 16.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Beef Tacos",
                    Description = "Three soft tacos filled with seasoned beef, lettuce, cheese, and salsa.",
                    Category = "Mexican",
                    Quantity = 1,
                    Price = 10.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Clam Chowder",
                    Description = "Creamy chowder with clams, potatoes, and celery.",
                    Category = "Soup",
                    Quantity = 1,
                    Price = 7.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Veggie Burger",
                    Description = "Plant-based burger patty with lettuce, tomato, and onion on a whole wheat bun.",
                    Category = "Vegetarian",
                    Quantity = 1,
                    Price = 9.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Chocolate Cake",
                    Description = "Decadent chocolate cake with rich chocolate frosting.",
                    Category = "Dessert",
                    Quantity = 1,
                    Price = 6.99m,
                }
            );
    }
}
