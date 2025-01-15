using Microsoft.AspNetCore.Mvc;
using SklepInternetowyMVC.Data;
using SklepInternetowyMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SklepInternetowyMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja GET do tworzenia książki
        public IActionResult Create()
        {
            var genres = _context.Genres.ToList();
            ViewData["Genres"] = genres; // Przypisanie listy gatunków do ViewData
            return View();
        }

        // Akcja POST do tworzenia książki
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            // Logowanie przesyłanych danych
            Console.WriteLine($"BookName: {book.BookName}, AuthorName: {book.AuthorName}, Price: {book.Price}, GenreId: {book.GenreId}");

            // Sprawdzenie poprawności modelu
            if (ModelState.IsValid)
            {
                try
                {
                    // Dodanie książki do bazy
                    _context.Add(book);

                    // Zapisanie zmian w bazie danych
                    await _context.SaveChangesAsync();

                    // Potwierdzenie zapisu i przekierowanie
                    Console.WriteLine("Book saved successfully!");

                    return RedirectToAction(nameof(ManageBooks));
                }
                catch (Exception ex)
                {
                    // Logowanie błędu, jeśli zapis się nie powiedzie
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // W przypadku niepowodzenia, zwróć formularz z błędami
            var genres = _context.Genres.ToList();
            ViewData["Genres"] = genres;
            return View(book);
        }


        // Akcja do zarządzania książkami
        public IActionResult ManageBooks()
        {
            var books = _context.Books.Include(b => b.Genre).ToList();
            return View(books);
        }
    }
}
