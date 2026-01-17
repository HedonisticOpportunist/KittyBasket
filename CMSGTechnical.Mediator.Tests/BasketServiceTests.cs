using CMSGTechnical.Mediator.Dtos;
using CMSGTechnical;

namespace CMSGTechnical.Mediator.Tests
{
    public class BasketServiceTests
    {
        [Fact]
        public void TestThatItemCanBeAddedToBasket()
        {
            var basket = new BasketService();
            var item = new MenuItemDto
            {
                Id = 1,
                Name = "Cake",
                Price = 3,
            };
            basket.Add(item);
            Assert.Equal(1, basket.Items.Count);
        }
    }
}
