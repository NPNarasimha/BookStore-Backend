﻿using System;
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
        public List<BooksModel> SortByPrice(string order);
        //public List<BooksModel> SearchBooksOnAuthor(string author);
        public List<BooksModel> GetRecentAddBook();
        public List<BooksModel> Search(string name);
    }
}
