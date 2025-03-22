namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    public class FAQItem
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
