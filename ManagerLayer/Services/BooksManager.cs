using System;
using System.Collections.Generic;
using System.Text;
using ManagerLayer.Interfaces;
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
    }
}
