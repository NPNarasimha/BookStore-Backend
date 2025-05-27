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
        public bool AddBook(AddBookModel model);
        public bool UpdateBook(int id, AddBookModel model);
        public bool DeleteBook(int id);
        public List<BooksModel> SortByPrice(string order);
       // public List<BooksModel> SearchBooksOnAuthor(string author);
        public List<BooksModel> GetRecentAddBook();
        public List<BooksModel> Search(string name);

        public List<BooksModel> StoredProcGetAllBooks();
        public bool AddBookWithStoredProc(AddBookModel model);
        public BooksModel GetBookByIdProc(int id);
       
    }
}
