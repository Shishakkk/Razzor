using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class ReadersModel : PageModel
    {
        private readonly LibraryService _lib;
        public ReadersModel(LibraryService lib) => _lib = lib;

        public List<Reader> Readers { get; set; } = new();

        [BindProperty] public string NewName { get; set; } = "";
        [BindProperty] public string NewEmail { get; set; } = "";

        public void OnGet() => Readers = _lib.GetReaders();

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewName))
                _lib.AddReader(new Reader { FullName = NewName, Email = NewEmail });
            return RedirectToPage();
        }
    }
}
