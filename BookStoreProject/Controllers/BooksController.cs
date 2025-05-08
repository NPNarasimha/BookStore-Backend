using System.IO;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var path=Path.Combine(Directory.GetCurrentDirectory(),"BooksCsvFile", "BooksList.csv");

            if (!System.IO.File.Exists(path))
            {
                return NotFound(new ResponseModel<string> { Success = false, Message = "File not found" });
            }
            booksManager.UploadBooksFromCSV(path);
            return Ok(new ResponseModel<string> { Success = true, Message = "Books uploaded successfully in the DB" });

        }

    }
}
