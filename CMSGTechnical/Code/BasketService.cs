using CMSGTechnical.Code;
using CMSGTechnical.Mediator.Dtos;

namespace CMSGTechnical.Mediator.Tests
{
    public class BasketServiceTests
    {
        [Fact]
        public async Task TestThatItemCanBeAddedToBasket()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            await basket.Add(item);

            Assert.Single(basket.Basket.MenuItems);
        }

        [Fact]
        public async Task TestThatItemCanBeRemovedFromBasket()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            await basket.Add(item);
            await basket.Remove(item);

            Assert.Empty(basket.Basket.MenuItems);
        }

        [Fact]
        public async Task TestThatSubtotalAndTotalAreCalculatedCorrectly()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item1 = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            var item2 = new MenuItemDto
            {
                Id = 2,
                Name = "Pie",
                Price = 5,
            };

            await basket.Add(item1);
            await basket.Add(item2);

            Assert.Equal(8.00m, basket.Subtotal);
            Assert.Equal(10.00m, basket.Total); // Subtotal + 2.00 fee
        }

        [Fact]
        public async Task TestThatOnChangeEventIsTriggered()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            bool eventTriggered = false;
            basket.OnChange += (sender, args) =>
            {
                eventTriggered = true;
                Assert.NotNull(args);
                Assert.NotNull(args.Basket);
            };

            await basket.Add(item);

            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task TestThatRemovingNonExistentItemDoesNotThrow()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            var exception = await Record.ExceptionAsync(() => basket.Remove(item));

            Assert.Null(exception);
        }

        [Fact]
        public async Task TestThatAddingSameItemIncreasesQuantity()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            await basket.Add(item);
            await basket.Add(item);

            var addedItem = basket.Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            Assert.NotNull(addedItem);
            Assert.Equal(2, addedItem.Quantity);
        }

        [Fact]
        public async Task TestThatRemovingItemDecreasesQuantity()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            await basket.Add(item);
            await basket.Add(item);
            await basket.Remove(item);

            var remainingItem = basket.Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            Assert.NotNull(remainingItem);
            Assert.Equal(1, remainingItem.Quantity);
        }

        [Fact]
        public async Task TestThatRemovingItemToZeroRemovesItFromBasket()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            await basket.Add(item);
            await basket.Remove(item);

            var removedItem = basket.Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            Assert.Null(removedItem);
        }

        [Fact]
        public async Task TestThatFeeIsConstant()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            Assert.Equal(2.00m, basket.Fee);
        }

        [Fact]
        public async Task TestThatBasketStartsEmpty()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            Assert.Empty(basket.Basket.MenuItems);
        }

        [Fact]
        public async Task TestThatMultipleItemsCanBeAdded()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item1 = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            var item2 = new MenuItemDto
            {
                Id = 2,
                Name = "Pie",
                Price = 5,
            };

            await basket.Add(item1);
            await basket.Add(item2);

            Assert.Equal(2, basket.Basket.MenuItems.Count);
        }

        [Fact]
        public async Task TestThatBasketIsUpdatedWhenItemsAreAdded()
        {
            var basketDto = new BasketDto();
            var basket = new BasketService(basketDto);

            var item1 = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };

            var item2 = new MenuItemDto
            {
                Id = 2,
                Name = "Pie",
                Price = 5,
            };

            await basket.Add(item1);
            await basket.Add(item2);

            Assert.Equal(2, basket.Basket.MenuItems.Count);
        }
    }
}
