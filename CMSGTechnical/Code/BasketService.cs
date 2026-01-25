using CMSGTechnical.Mediator.Dtos;
using Microsoft.JSInterop;

namespace CMSGTechnical.Code
{
    public class BasketChangedEventArgs : EventArgs
    {
        // Event payload containing the updated basket.
        public BasketDto? Basket { get; set; }
    }

    public class BasketService
    {
        private readonly IJSRuntime _js; 
        // Reference to the JavaScript runtime, used for calling JS functions (e.g., localStorage access).

        private bool _initializedFromJs = false;
        // Ensures the basket is only loaded from JavaScript once per circuit.

        // Calculated values based on current basket contents.
        public decimal Subtotal => Basket.MenuItems.Sum(i => i.Price * i.Quantity);
        public decimal Fee => 2.00m;
        public decimal Total => Subtotal + Fee;

        // Event fired whenever the basket changes.
        // Initialized with an empty delegate so it can be invoked safely.
        public event EventHandler<BasketChangedEventArgs?> OnChange = delegate { };

        // The current basket state.
        public BasketDto Basket { get; private set; }

        public BasketService(BasketDto basket, IJSRuntime js)
        {
            // Constructor receives the initial basket data and the JS runtime reference.
            Basket = basket;
            _js = js;
        }

        // Loads the basket from localStorage AFTER the Blazor circuit is ready.
        // Prevents overwriting server‑provided state too early.
        public async Task InitializeFromJsAsync()
        {
            if (_initializedFromJs)
                return;

            var loaded = await _js.InvokeAsync<BasketDto>("basketStore.load");

            // Only replace the basket if something meaningful was stored.
            if (loaded?.MenuItems?.Any() == true)
                Basket = loaded;

            _initializedFromJs = true;

            // Notify subscribers that the basket has been initialized/updated.
            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket });
        }

        // Adds an item to the basket and persists the updated state to localStorage.
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

            // Persist updated basket to localStorage.
            await _js.InvokeVoidAsync("basketStore.save", Basket);

            // Notify listeners of the change.
            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket });
        }

        // Removes an item from the basket and persists the updated state.
        public async Task Remove(MenuItemDto item)
        {
            var existing = Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing is null)
                return;

            existing.Quantity -= 1;

            // Remove the item entirely if quantity reaches zero.
            if (existing.Quantity <= 0)
                Basket.MenuItems.Remove(existing);

            // Persist updated basket to localStorage.
            await _js.InvokeVoidAsync("basketStore.save", Basket);

            // Notify listeners of the change.
            OnChange?.Invoke(this, new BasketChangedEventArgs { Basket = Basket });
        }
    }
}
