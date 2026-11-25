using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Ordering;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            Guid customerId = new Guid("08570594-1d2f-402b-987a-0d6b7fb4656f");

            var response = await orderingService.GetOrderByCustomer(customerId);
            Orders = response.Orders;

            return Page();
        }
    }
}
