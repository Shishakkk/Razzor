using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class LibraryService
    {
        private readonly List<Book> _books = new();
        private readonly List<Reader> _readers = new();
        private readonly List<Rental> _rentals = new();
        private int _bookId = 1, _readerId = 1, _rentalId = 1;

        public LibraryService()
        {
            
            _books.Add(new Book { Id = _bookId++, Title = "Война и мир", Author = "Л. Толстой", Year = 1869 });
            _books.Add(new Book { Id = _bookId++, Title = "Преступление и наказание", Author = "Ф. Достоевский", Year = 1866 });
            _books.Add(new Book { Id = _bookId++, Title = "Мастер и Маргарита", Author = "М. Булгаков", Year = 1967 });

            _readers.Add(new Reader { Id = _readerId++, FullName = "Иван Иванов", Email = "ivan@mail.ru" });
            _readers.Add(new Reader { Id = _readerId++, FullName = "Пётр Петров", Email = "petr@mail.ru" });
        }

     
        public List<Book> GetBooks() => _books.ToList();

        public void AddBook(Book book)
        {
            book.Id = _bookId++;
            book.IsAvailable = true;
            _books.Add(book);
        }

       
        public List<Reader> GetReaders() => _readers.ToList();

        public void AddReader(Reader reader)
        {
            reader.Id = _readerId++;
            _readers.Add(reader);
        }

        
        public List<Rental> GetRentals() => _rentals.ToList();

        public Rental? GetActiveRentalByBook(int bookId)
            => _rentals.FirstOrDefault(r => r.BookId == bookId && r.IsActive);

        public bool RentBook(int bookId, int readerId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null || !book.IsAvailable) return false;

            var rental = new Rental
            {
                Id = _rentalId++,
                BookId = bookId,
                ReaderId = readerId,
                RentedAt = DateTime.Now
            };
            _rentals.Add(rental);
            book.IsAvailable = false;
            return true;
        }

        public bool ReturnBook(int rentalId)
        {
            var rental = _rentals.FirstOrDefault(r => r.Id == rentalId && r.IsActive);
            if (rental == null) return false;

            rental.ReturnedAt = DateTime.Now;
            var book = _books.FirstOrDefault(b => b.Id == rental.BookId);
            if (book != null) book.IsAvailable = true;
            return true;
        }

        public Book? GetBook(int id) => _books.FirstOrDefault(b => b.Id == id);
        public Reader? GetReader(int id) => _readers.FirstOrDefault(r => r.Id == id);
    }
}
