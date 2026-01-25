window.basketStore = {
  // The localStorage object stores data with no expiration date.
  // In this case, the items in the basket remain saved even after the browser is closed.
  save: (basket) => localStorage.setItem("basket", JSON.stringify(basket)),

  // The "load" method retrieves the saved basket from localStorage.
  // If nothing has been saved, it returns an empty object instead.
  load: () => JSON.parse(localStorage.getItem("basket") || "{}"),
};
