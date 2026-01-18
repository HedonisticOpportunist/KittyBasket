window.basketStore = {
  save: (basket) => localStorage.setItem("basket", JSON.stringify(basket)),
  load: () => JSON.parse(localStorage.getItem("basket") || "{}"),
};
