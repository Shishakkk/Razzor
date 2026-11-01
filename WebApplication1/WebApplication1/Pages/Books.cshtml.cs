using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class BooksModel : PageModel
    {
        private readonly LibraryService _lib;
        public BooksModel(LibraryService lib) => _lib = lib;

        public List<Book> Books { get; set; } = new();
        public List<Reader> Readers { get; set; } = new();

        [BindProperty] public string NewTitle { get; set; } = "";
        [BindProperty] public string NewAuthor { get; set; } = "";
        [BindProperty] public int NewYear { get; set; }
        [BindProperty] public int SelectedReaderId { get; set; }

        public void OnGet() => Reload();

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrWhiteSpace(NewTitle) && !string.IsNullOrWhiteSpace(NewAuthor))
                _lib.AddBook(new Book { Title = NewTitle, Author = NewAuthor, Year = NewYear });
            return RedirectToPage();
        }

        public IActionResult OnPostRent(int bookId)
        {
            if (SelectedReaderId > 0)
                _lib.RentBook(bookId, SelectedReaderId);
            return RedirectToPage();
        }

        private void Reload()
        {
            Books = _lib.GetBooks();
            Readers = _lib.GetReaders();
        }
    }
}
