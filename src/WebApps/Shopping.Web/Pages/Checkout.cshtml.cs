using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();
            return Page();
        }
        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            logger.LogInformation("Checkout basket button clicked");
            Cart = await basketService.LoadUserBasket();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.CustomerId = new Guid("08570594-1d2f-402b-987a-0d6b7fb4656f");
            Order.UserName = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;

            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
