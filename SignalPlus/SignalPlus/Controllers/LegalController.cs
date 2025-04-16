namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class LegalController : Controller
    {
        [Route("rules")]
        public IActionResult Rules()
        {
            return View();
        }

        [Route("terms")]
        public IActionResult Terms()
        {
            return View();
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
