using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class ProductDetailModel
        (ICatalogService catalogService, IBasketService basketService,
        ILogger<ProductDetailModel> logger) : PageModel
    {
        public ProductModel Product { get; set; } = default!;
        [BindProperty]
        public string Color { get; set; } = default!;
        [BindProperty]
        public int Quantity { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProduct(productId);
            Product = response.product;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add product to the cart button clicked");

            var productResponse = await catalogService.GetProduct(productId);
            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.product.Name,
                Price = productResponse.product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
