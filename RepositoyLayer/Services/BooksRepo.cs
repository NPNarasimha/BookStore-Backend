using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoyLayer.Context;
using RepositoyLayer.Entity;
using RepositoyLayer.Interfaces;

namespace RepositoyLayer.Services
{
    public class BooksRepo:IBooksRepo
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
       
    }
}
