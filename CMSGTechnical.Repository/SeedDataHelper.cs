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
                    Name = "Whisker-Lickin’ Chicken Purr‑zza",
                    Description = "A cozy pizza topped with tender chicken bites, mozzarella, and a sprinkle of catnip‑inspired herbs.",
                    Category = "Purr‑zza",
                    Quantity = 1,
                    Price = 12.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Kitty Caesar Salad",
                    Description = "Crisp greens tossed with creamy dressing, crunchy ‘crouton kibble’, and parmesan flakes.",
                    Category = "Salads for Good Cats",
                    Quantity = 1,
                    Price = 8.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Grilled Salmon for Fancy Felines",
                    Description = "A perfectly grilled salmon fillet served with roasted garden veggies — a true cat delicacy.",
                    Category = "Fish Feast",
                    Quantity = 1,
                    Price = 18.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Creamy Paw‑bonara",
                    Description = "Silky pasta coated in a creamy sauce with crispy pancetta and a dash of cracked pepper.",
                    Category = "Cat‑sta",
                    Quantity = 1,
                    Price = 14.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Garden Meow‑dl Stir Fry",
                    Description = "A colorful medley of stir‑fried veggies served with fluffy rice — perfect for herbivore kitties.",
                    Category = "Veggie Delights",
                    Quantity = 1,
                    Price = 11.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Paw‑mesan Chicken Delight",
                    Description = "Crispy breaded chicken topped with marinara and melted cheese, served with a side of pasta.",
                    Category = "Main Purr‑course",
                    Quantity = 1,
                    Price = 16.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Beefy Meow‑cos",
                    Description = "Three soft tacos stuffed with seasoned beef, shredded greens, cheese, and zesty salsa.",
                    Category = "Meow‑xican",
                    Quantity = 1,
                    Price = 10.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Clawm Chowder",
                    Description = "A warm, creamy chowder filled with clams, potatoes, and celery — perfect for cold cat days.",
                    Category = "Soups & Slurps",
                    Quantity = 1,
                    Price = 7.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Purr‑fect Veggie Burger",
                    Description = "A hearty plant‑based patty with fresh toppings on a whole‑grain bun — feline‑friendly and filling.",
                    Category = "Veggie Delights",
                    Quantity = 1,
                    Price = 9.99m,
                },
                new
                {
                    Id = id++,
                    Order = 0,
                    Name = "Chocolate Cat‑nip Cake",
                    Description = "A decadent chocolate dessert layered with rich frosting — strictly for humans, not actual cats.",
                    Category = "Desserts for Hoomans",
                    Quantity = 1,
                    Price = 6.99m,
                }
            );
    }
}
