using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private const string ScoreKey = "Score";

        [BindProperty]
        public int Score { get; set; }

        public void OnGet()
        {
            Score = HttpContext.Session.GetInt32(ScoreKey) ?? 0;
        }

        
        public IActionResult OnPostClick()
        {
            var score = (HttpContext.Session.GetInt32(ScoreKey) ?? 0) + 1;
            HttpContext.Session.SetInt32(ScoreKey, score);
            return new JsonResult(new { score });
        }

       
        public IActionResult OnPostReset()
        {
            HttpContext.Session.SetInt32(ScoreKey, 0);
            return new JsonResult(new { score = 0 });
        }
    }
}
