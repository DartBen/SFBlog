using BlogApp.DLL.Models;
using BlogApp.Views;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Errors/{id?}")]
        public async Task<IActionResult> ErrorsRedirect(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                switch (statusCode)
                {
                    case 400: return RedirectToPage("/Errors/ResourceIsNotFoundPage");
                    case 401: return RedirectToPage("/Errors/AccessIsDeniedPage");
                    default: return RedirectToPage("/Errors/SomethingWrongPage");
                }
            }
            return RedirectToPage("/Error");
        }
        [Route("MakeError")]
        public IActionResult MakeError()
        {
            int a = 0;
            var temp = 42 / a;

            return StatusCode(402);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
