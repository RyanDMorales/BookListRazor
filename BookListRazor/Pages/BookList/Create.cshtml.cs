using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }


        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if(_db.Book.Any(b => b.ISBN == Book.ISBN)){
                Message = "Book already exists!";
                return RedirectToPage("Create");
            }

            _db.Book.Add(Book);

            await _db.SaveChangesAsync();

            Message = "Book created successfully!";

            return RedirectToPage("Index");
        }
    }
}
