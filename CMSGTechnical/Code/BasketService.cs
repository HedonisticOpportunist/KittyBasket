using CMSGTechnical.Mediator.Dtos;

namespace CMSGTechnical.Code
{
    public class BasketChangedEventArgs : EventArgs
    {
        public BasketDto Basket { get; set; }
    }

    public class BasketService
    {
        public decimal Subtotal => Basket.MenuItems.Sum(i => i.Price * i.Quantity); // Calculate subtotal based on items in the basket.
        public decimal Total => Subtotal + 2.00m; // Total includes a fixed (delivery) fee.
        public decimal Fee => 2.00m;
        public event EventHandler<BasketChangedEventArgs> OnChange;

        public BasketDto Basket { get; }

        public BasketService(BasketDto basket)
        {
            Basket = basket;
        }

        public async Task Add(MenuItemDto item)
        {
            var existing = Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                Basket.MenuItems.Add(
                    new MenuItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Category = item.Category,
                        Description = item.Description,
                        Quantity = 1,
                    }
                );
            }
            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket }); // Handle change even if null.
        }

        public async Task Remove(MenuItemDto item)
        {
            var existing = Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing is null)
                return;

            existing.Quantity -= 1;

            if (existing.Quantity <= 0)
                Basket.MenuItems.Remove(existing);
            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket }); // Handle change even if null.
        }
    }
}
