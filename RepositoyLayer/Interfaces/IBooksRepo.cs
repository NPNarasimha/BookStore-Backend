using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using RepositoyLayer.Entity;

namespace RepositoyLayer.Interfaces
{
    public interface IBooksRepo
    {
        public void UploadBooksFromCSV(string path);
        public List<BooksModel> GetAllBooks();
        public BooksModel GetBookById(int id);
        public bool AddBook(BooksModel model);
        public bool UpdateBook(int id, BooksModel model);
        public bool DeleteBook(int id);
        public List<BooksModel> SortByPrice(string order);
        public List<BooksModel> SearchBooksOnAuthor(string author);
    }
}
