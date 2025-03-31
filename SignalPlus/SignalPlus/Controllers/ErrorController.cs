namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }

        [Route("Error/{code}")]
        public IActionResult Error(int code)
        {
            // For any other error, redirect to home
            return RedirectToAction("Index", "Home");
        }
    }
}
