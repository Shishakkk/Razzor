using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class RentalsModel : PageModel
    {
        private readonly LibraryService _lib;
        public RentalsModel(LibraryService lib) => _lib = lib;

        public List<RentalView> Rentals { get; set; } = new();

        public void OnGet()
        {
            Rentals = _lib.GetRentals()
                .OrderByDescending(r => r.RentedAt)
                .Select(r => new RentalView
                {
                    Id = r.Id,
                    BookTitle = _lib.GetBook(r.BookId)?.Title ?? "—",
                    ReaderName = _lib.GetReader(r.ReaderId)?.FullName ?? "—",
                    RentedAt = r.RentedAt,
                    ReturnedAt = r.ReturnedAt
                }).ToList();
        }

        public IActionResult OnPostReturn(int rentalId)
        {
            _lib.ReturnBook(rentalId);
            return RedirectToPage();
        }

        public class RentalView
        {
            public int Id { get; set; }
            public string BookTitle { get; set; } = "";
            public string ReaderName { get; set; } = "";
            public DateTime RentedAt { get; set; }
            public DateTime? ReturnedAt { get; set; }
        }
    }
}
