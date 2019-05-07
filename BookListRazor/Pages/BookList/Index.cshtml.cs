using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.OrderBy(b => b.Name).ToListAsync();
        }


        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if(book == null)
            {
                Message = "Book does not exist or has already been deleted!";
                return RedirectToPage("Index");
            }

            _db.Book.Remove(book);

            await _db.SaveChangesAsync();
            
            Message = "Book deleted successfully!";

            return RedirectToPage("Index");
        }
    }
}
