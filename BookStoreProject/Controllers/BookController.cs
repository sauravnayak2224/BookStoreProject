using BookStoreProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private static List<Book> books = new List<Book>()
        {
        new Book { BookID = "9b0896fa-3880-4c2e-bfd6-925c87f22878", BookTitle = "CQRS for Dummies", IsBookReserved = false },
        new Book { BookID = "0550818d-36ad-4a8d-9c3a-a715bf15de76", BookTitle = "Visual Studio Tips", IsBookReserved = false },
        new Book { BookID = "8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1", BookTitle = "NHibernate Cookbook", IsBookReserved = false }
        };

        public ActionResult Index()
        {
            return View(books);
        }

        public ActionResult Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b => b.BookTitle.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            TempData["message"] = $"Search results for '{searchTerm}': {books.Count} books found.";

            return View("Index", books);
        }

        public ActionResult Reserve(String id)
        {
            var book = books.FirstOrDefault(b => b.BookID == id);
            if (book == null)
            {
                return View("Error");
            }

            book.IsBookReserved = true;
            TempData["message"] = "Book reserved successfully. Your booking number is: " + Guid.NewGuid();
            return RedirectToAction("Index");

        }


        public ActionResult UnReserve(String id)
        {
            var book = books.FirstOrDefault(b => b.BookID == id);
            if (book == null)
            {
                return View("Error");
            }
            book.IsBookReserved = false;
            TempData["message"] = "Book Unreserved successfully.";
            return RedirectToAction("Index");

        }

    }

}
