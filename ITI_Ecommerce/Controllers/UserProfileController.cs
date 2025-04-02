using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult GetUserProfile()
        {
            return View();
        }
    }
}
