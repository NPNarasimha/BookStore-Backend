using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoyLayer.Context;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class BooksRepo : IBooksRepo
    {
        private readonly BookStoreDBContext context;
        public BooksRepo(BookStoreDBContext context)
        {
            this.context = context;
        }

        public void UploadBooksFromCSV(string path)
        {
            var csvData = System.IO.File.ReadAllLines(path).Skip(1);
            foreach (var line in csvData)
            {
                var values = line.Split(',').Select(i => i.Trim()).ToArray();
                if (values.Length < 11)
                {
                    Console.WriteLine($"Skipped line (less than 11 fields): {line}");
                    continue;
                }
                if (!int.TryParse(values[4], out int price) ||
                    !int.TryParse(values[5], out int discountPrice) ||
                    !int.TryParse(values[6], out int quantity) ||
                    !DateTime.TryParse(values[9], out DateTime createdAt) ||
                    !DateTime.TryParse(values[10], out DateTime updatedAt))
                {
                    Console.WriteLine($"Skipped line (invalid format): {line}");
                    continue;
                }
                var book = new Books
                {
                    BookName = values[1],
                    Author = values[2],
                    Description = values[3],
                    Price = price,
                    DiscountPrice = discountPrice,
                    Quantity = quantity,
                    BookImage = values[7],
                    AdminUserId = values[8],
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt
                };
                context.Books.Add(book);
            }
            context.SaveChanges();
            Console.WriteLine("CSV data loaded successfully.");
        }


        public List<BooksModel> GetAllBooks()
        {
            return context.Books.OrderBy(b => b.BookName).Select(b => new BooksModel
            {
                BookName = b.BookName,
                Author = b.Author,
                Description = b.Description,
                Price = b.Price,
                DiscountPrice = b.DiscountPrice,
                Quantity = b.Quantity,
                BookImage = b.BookImage,
                AdminUserId = b.AdminUserId,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            })
         .ToList();
        }
        public BooksModel GetBookById(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return null;
            }
            return new BooksModel
            {
                BookName = book.BookName,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price,
                DiscountPrice = book.DiscountPrice,
                Quantity = book.Quantity,
                BookImage = book.BookImage,
                AdminUserId = book.AdminUserId,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };
        }
        public bool AddBook(BooksModel model)
        {
            var book = new Books
            {
                BookName = model.BookName,
                Author = model.Author,
                Description = model.Description,
                Price = (int)model.Price,
                DiscountPrice = (int)model.DiscountPrice,
                Quantity = model.Quantity,
                BookImage = model.BookImage,
                AdminUserId = model.AdminUserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
             context.Books.Add(book);
            return context.SaveChanges()>0;
        }
        public bool UpdateBook(int id, BooksModel model)
        {
            var book = context.Books.ToList().Find(x => x.BookId == id);
            if (book != null)
            {
                book.BookName = model.BookName;
                book.Author = model.Author;
                book.Description = model.Description;
                book.Price = (int)model.Price;
                book.DiscountPrice = (int)model.DiscountPrice;
                book.Quantity = model.Quantity;
                book.BookImage = model.BookImage;
                book.AdminUserId = model.AdminUserId;
                book.UpdatedAt = DateTime.Now;
                context.Books.Update(book);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteBook(int id)
        {
            var book = context.Books.ToList().Find(x => x.BookId == id);
            if (book == null)
            {
                return false;
            }
            else
            {
                context.Books.Remove(book);
                context.SaveChanges();
                return true;
            }
        }

        public List<BooksModel> SortByPrice(string order)
        {
            var bookOrder = order.ToLower() == "high" ? context.Books.OrderByDescending(b => b.Price).ToList() : context.Books.OrderBy(b => b.Price).ToList();
            return bookOrder.Select(b => new BooksModel{
                BookName = b.BookName,
                Author = b.Author,
                Description = b.Description,
                Price = b.Price,
                DiscountPrice = b.DiscountPrice,
                Quantity = b.Quantity,
                BookImage = b.BookImage,
                AdminUserId = b.AdminUserId,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            }).ToList();

        }

        //public List<BooksModel> SearchBooksOnAuthor(string author)
        //{
        //    var books=context.Books.Where(b=>b.Author.ToLower().Contains(author.ToLower())).ToList();
        //    return books.Select(b => new BooksModel
        //    {
        //        BookName = b.BookName,
        //        Author = b.Author,
        //        Description = b.Description,
        //        Price = b.Price,
        //        DiscountPrice = b.DiscountPrice,
        //        Quantity = b.Quantity,
        //        BookImage = b.BookImage,
        //        AdminUserId = b.AdminUserId,
        //        CreatedAt = b.CreatedAt,
        //        UpdatedAt = b.UpdatedAt
        //    }).ToList();
        //}

        public List<BooksModel> Search(string name)
        {
            var books=context.Books.Where(b=>b.BookName.ToLower().Contains(name)||b.Author.ToLower().Contains(name)).ToList().
                Select(b => new BooksModel
                {
                    BookName = b.BookName,
                    Author = b.Author,
                    Description = b.Description,
                    Price = b.Price,
                    DiscountPrice = b.DiscountPrice,
                    Quantity = b.Quantity,
                    BookImage = b.BookImage,
                    AdminUserId = b.AdminUserId,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt
                }).ToList();
            return books;

        }
        public List<BooksModel> GetRecentAddBook()
        {
            var recentBook = context.Books.OrderByDescending(b => b.CreatedAt).Take(1).Select(b => new BooksModel
            {
                BookName = b.BookName,
                Author = b.Author,
                Description = b.Description,
                Price = b.Price,
                DiscountPrice = b.DiscountPrice,
                Quantity = b.Quantity,
                BookImage = b.BookImage,
                AdminUserId = b.AdminUserId,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            }).ToList();
            return recentBook;
        }
    }
}
