using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace ManagerLayer.Interfaces
{
    public interface IBooksManager
    {
        public void UploadBooksFromCSV(string path);
        public List<BooksModel> GetAllBooks();
        public bool AddBook(BooksModel model);
        public bool UpdateBook(int id, BooksModel model);
    }
}
