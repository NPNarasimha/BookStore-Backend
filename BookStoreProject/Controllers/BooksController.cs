using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoyLayer.Entity;

namespace BookStoreProject.Controllers
{
    [Route("")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksManager booksManager;
        public BooksController(IBooksManager booksManager)
        {
            this.booksManager = booksManager;
        }

        [Authorize]
        [HttpPost("uploadBooks")]
        public IActionResult UploadingBooks()
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role == "User")
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Only the admin can upload books" });
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "BooksCsvFile", "BooksList.csv");

            if (!System.IO.File.Exists(path))
            {
                return NotFound(new ResponseModel<string> { Success = false, Message = "File not found" });
            }
            booksManager.UploadBooksFromCSV(path);
            return Ok(new ResponseModel<string> { Success = true, Message = "Books uploaded successfully in the DB" });
        }

        [Authorize]
        [HttpGet("getAllBooks")]
        public IActionResult GetAllBooks()
        {

            var books = booksManager.GetAllBooks();
            if (books.Count == 0)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "No books" });
            }
            return Ok(new ResponseModel<List<BooksModel>> { Success = true, Message = "All books", Data = books });
        }
        [Authorize]
        [HttpGet("getbookbyid")]
        public IActionResult GetBookById(int id)
        {
            var book = booksManager.GetBookById(id);
            if (book == null)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Book not found" });
            }
            return Ok(new ResponseModel<BooksModel> { Success = true, Message = "Book found", Data = book });
        }

            [Authorize]
        [HttpPost("addbook")]
        public IActionResult AddBook(BooksModel model)
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role == "User")
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Only the admin can add books" });
            }
            if (booksManager.AddBook(model))
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Book added successfully" });
            }
            return BadRequest(new ResponseModel<string> { Success = false, Message = "Book Is Not Added" });
        }

        [Authorize]
        [HttpPut("updatebook")]
        public IActionResult UpdateBook(int id, BooksModel model)
        {
            var role=User.FindFirst("custom_role")?.Value;
            if (role == "User")
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Only the admin can update books" });
            }
            if (booksManager.UpdateBook(id, model))
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Book updated successfully" });
            }
            return BadRequest(new ResponseModel<string> { Success = false, Message = "Book Is Not Updated" });
        }
        [Authorize]
        [HttpDelete("deletebook")]
        public IActionResult DeleteBook(int id)
        {
            var role = User.FindFirst("custom_role")?.Value;
            if (role == "User")
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Only the admin can delete books" });
            }
            if (booksManager.DeleteBook(id))
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Book deleted successfully" });
            }
            return BadRequest(new ResponseModel<string> { Success = false, Message = "Book Is Not Deleted" });
        }
    }
}
