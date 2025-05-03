namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View(FaqConstants.All);
        }
    }
}
