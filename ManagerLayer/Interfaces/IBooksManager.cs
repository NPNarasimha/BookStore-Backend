using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface IBooksManager
    {
        public void UploadBooksFromCSV(string path);
        public List<BooksModel> GetAllBooks();
        public bool AddBook(BooksModel model);
        public bool UpdateBook(int id, BooksModel model);
        public bool DeleteBook(int id);
        public BooksModel GetBookById(int id);
    }
}
