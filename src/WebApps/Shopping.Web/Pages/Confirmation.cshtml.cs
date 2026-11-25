using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;
        public void OnGet()
        {
            Message = "Your email was sent.";
        }
        public void OnGetOrderSubmitted()
        {
            Message = "Your order placed successfully.";
        }
    }
}
