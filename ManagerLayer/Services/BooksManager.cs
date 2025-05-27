using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class BooksManager:IBooksManager
    {
        private readonly IBooksRepo booksRepo;
        public BooksManager(IBooksRepo booksRepo)
        {
            this.booksRepo = booksRepo;
        }
        public void UploadBooksFromCSV(string path)
        {
            booksRepo.UploadBooksFromCSV(path);
        }
        public List<BooksModel> GetAllBooks()
        {
            return booksRepo.GetAllBooks();
        }
        public bool AddBook(AddBookModel model)
        {
            return booksRepo.AddBook(model);
        }
        public bool UpdateBook(int id, AddBookModel model)
        {
            return booksRepo.UpdateBook(id, model);
        }
        public bool DeleteBook(int id) 
        { 
            return booksRepo.DeleteBook(id);
        }
        public BooksModel GetBookById(int id)
        {
            return booksRepo.GetBookById(id);
        }
        public List<BooksModel> SortByPrice(string order)
        {
            return booksRepo.SortByPrice(order);
        }
        //public List<BooksModel> SearchBooksOnAuthor(string author)
        //{
        //    return booksRepo.SearchBooksOnAuthor(author);
        //}
        public List<BooksModel> GetRecentAddBook()
        {
            return booksRepo.GetRecentAddBook();
        }
        public List<BooksModel> Search(string name)
        {
            return booksRepo.Search(name);
        }
        public List<BooksModel> StoredProcGetAllBooks()
        {
            return booksRepo.StoredProcGetAllBooks();
        }
        public bool AddBookWithStoredProc(AddBookModel model)
        {
            return booksRepo.AddBookWithStoredProc(model);
        }
        public BooksModel GetBookByIdProc(int id)
        {
            return booksRepo.GetBookByIdProc(id);

        }
        
    }
}
