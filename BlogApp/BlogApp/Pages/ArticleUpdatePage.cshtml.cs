using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages
{
    public class ArticleUpdatePageModel : PageModel
    {
        public object? Id { get; private set; }

        public void OnGet()
        {
            Id = RouteData.Values["id"];
        }
    }
}