using CMSGTechnical.Mediator.Dtos;
using Microsoft.JSInterop;

namespace CMSGTechnical.Code
{
    public class BasketChangedEventArgs : EventArgs
    {
        public BasketDto? Basket { get; set; }
    }

    public class BasketService
    {
        private readonly IJSRuntime _js;
        private bool _initializedFromJs = false;

        public decimal Subtotal => Basket.MenuItems.Sum(i => i.Price * i.Quantity);
        public decimal Total => Subtotal + 2.00m;
        public decimal Fee => 2.00m;

        public event EventHandler<BasketChangedEventArgs?> OnChange = delegate { };

        public BasketDto Basket { get; private set; }

        public BasketService(BasketDto basket, IJSRuntime js)
        {
            Basket = basket;
            _js = js;
        }

        // 🔹 Load basket from localStorage AFTER the circuit is ready
        public async Task InitializeFromJsAsync()
        {
            if (_initializedFromJs)
                return;

            var loaded = await _js.InvokeAsync<BasketDto>("basketStore.load");

            if (loaded?.MenuItems?.Any() == true)
                Basket = loaded;

            _initializedFromJs = true;

            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket });
        }

        // 🔹 Add item + persist
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

            await _js.InvokeVoidAsync("basketStore.save", Basket);

            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket });
        }

        // 🔹 Remove item + persist
        public async Task Remove(MenuItemDto item)
        {
            var existing = Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing is null)
                return;

            existing.Quantity -= 1;

            if (existing.Quantity <= 0)
                Basket.MenuItems.Remove(existing);

            await _js.InvokeVoidAsync("basketStore.save", Basket);

            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket });
        }
    }
}
