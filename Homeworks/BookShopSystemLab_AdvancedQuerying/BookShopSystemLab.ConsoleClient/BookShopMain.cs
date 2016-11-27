using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookShopsystemLab.Data;
using BookShopSystemsLab.Models;
using EntityFramework.Extensions;

namespace BookShopSystemLab.ConsoleClient
{
    public class BookShopMain
    {
        private static void Main(string[] args)
        {
            var context = new BookShopLabContext();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");
            //var count = context.Books.Count();

            // 2.	Books Titles by Age Restriction
            // GetBookTitleByAgeRestriction(context);

            // 3. Golden Books
            // GetGoldenBooks(context);

            // 4. Books By Price
            // GetBooksByPrice(context);

            // 5. Not Released Books
            // GetNotReleasedBook(context);

            // 6. BookTitleByCategory
            // BookTitleByCategory(context);

            // 7.Books Released Before Date
            // BooksReleasedBeforeDate(context);

            // 8. Authors Searched
            // Write a program that selects and prints names of those authors whose first name end with given string.
            // AuthorsSearch(context);

            // 9. Books Search
            // Write a program that selects and prints titles of books which contains given string (regardless of the casing).
            // BooksSearch(context);

            // 10. Book Titles Search
            // BookTitlesSearch(context);

            // 11. Count Books
            // CountBooks(context);

            // 12. Total Book Copies
            // TotalBookCopies(context);

            // 13. Find Profit
            // FindProfit(context);

            // 14. MostRecentBooks
            // MosteRecentBooks(context);

            // 15. Increase book copies
            //IncreaseBookCopies(context);

            // 16. Remove Books
            // RemoveBooks(context);
            
        }

        private static void RemoveBooks(BookShopLabContext context)
        {
            Console.WriteLine("Please enter number (int):");
            int number = int.Parse(Console.ReadLine());
            var books = context.Books
                .Where(b => b.Copies < number)
                .Delete();
            Console.WriteLine($"Deleted books: {books}");
            context.SaveChanges();
        }

        private static void IncreaseBookCopies(BookShopLabContext context)
        {
            Console.WriteLine("Please enter a date:");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Please enter book copies (int):");
            int bookCopies = int.Parse(Console.ReadLine());

            var books = context.Books
                .Where(b => b.ReleaseDate > date);
                

            Console.WriteLine("Result: ");
            Console.WriteLine(books.Count()*bookCopies);

            var updatedBooks = context.Books
                .Where(b => b.ReleaseDate > date)
                .Update(b => new Book() {Copies = b.Copies + bookCopies});
            Console.WriteLine("Updated Books: {0}", updatedBooks);
            context.SaveChanges();
        }

        private static void MosteRecentBooks(BookShopLabContext context)
        {
            var categories = context.Categories
                .Where(c => c.Books.Count > 35)
                .Select(c => new
                {
                    c.Name,
                    c.Books.Count,
                    RecentBooks = c.Books
                        .OrderByDescending(b => b.ReleaseDate)
                        .ThenBy(b => b.Title)
                        .Take(3)
                        .Select(b => new
                        {
                            b.Title,
                            b.ReleaseDate
                        })
                });
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name} - {category.Count} books");

                foreach (var book in category.RecentBooks)
                {
                    Console.WriteLine($"{book.Title} - {book.ReleaseDate}");
                }
                Console.WriteLine();
            }
        }

        private static void FindProfit(BookShopLabContext context)
        {
            var categories = context.Categories
                .GroupBy(c => new
                {
                    CategoryName = c.Name,
                    Profit = c.Books.Sum(b => b.Price*b.Copies)
                })
                .OrderByDescending(c => c.Key.Profit)
                .ThenBy(c => c.Key.CategoryName);

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Key.CategoryName} - ${category.Key.Profit}");
            }

        }

        private static void TotalBookCopies(BookShopLabContext context)
        {
            var authors = context.Authors
                .GroupBy(a => new
                {
                    Author = a.FirstName + " " + a.LastName,
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Key.Copies);

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Key.Author} - {author.Key.Copies}");
            }
        }

        private static void CountBooks(BookShopLabContext context)
        {
            Console.WriteLine("Please enter a number");
            int lenght = int.Parse(Console.ReadLine());

            var books = context.Books
                .Where(b => b.Title.Length > lenght);
            Console.WriteLine(books.Count());
        }

        private static void BookTitlesSearch(BookShopLabContext context)
        {
            Console.WriteLine("Please enter string");
            string startString = Console.ReadLine();

            var books = context.Books
                .Where(b => b.Author.LastName.StartsWith(startString))
                .Select(b => b.Title);
            PrintBooks(books);
        }

        private static void BooksSearch(BookShopLabContext context)
        {
            Console.WriteLine("Please enter string");
            string containingString = Console.ReadLine();

            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(containingString.ToLower()))
                .Select(b => b.Title);
            PrintBooks(books);
        }

        private static void AuthorsSearch(BookShopLabContext context)
        {
            Console.WriteLine("Please enter string");
            string endString = Console.ReadLine();

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(endString))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                });

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }
        }

        private static void BooksReleasedBeforeDate(BookShopLabContext context)
        {
            Console.WriteLine("Please enter date");
            DateTime date = DateTime.Parse(Console.ReadLine());
            var books = context.Books
                .Where(b => b.ReleaseDate < date)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                });

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.EditionType} - {book.Price}");
            }
        }

        private static void BookTitleByCategory(BookShopLabContext context)
        {
            Console.WriteLine("Enter categories separated by a space");
            var categories = Console.ReadLine().Split(' ').ToList();
            var books = context.Books
                .Where(b => b.Categories.Count(c => categories.Contains(c.Name)) != 0)
                .Select(b => b.Title);
            PrintBooks(books);
        }

        private static void GetNotReleasedBook(BookShopLabContext context)
        {
            Console.WriteLine("Please enter an year");
            int year = int.Parse(Console.ReadLine());
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => b.Title);
            PrintBooks(books);
        }

        private static void GetBooksByPrice(BookShopLabContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 5 && b.Price < 40)
                .Select(b => new
                {
                    title = b.Title,
                    price = b.Price
                });

            foreach (var book in books)
            {
                Console.WriteLine($"{book.title} - ${book.price}");
            }
        }

        private static void GetGoldenBooks(BookShopLabContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => b.Title);
            PrintBooks(books);
        }

        private static void GetBookTitleByAgeRestriction(BookShopLabContext context)
        {
            Console.WriteLine("Please enter a command:");
            string ageRestriction = Console.ReadLine().ToLower();

            var books = context.Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == ageRestriction)
                .Select(b => b.Title);
            PrintBooks(books);
        }

        private static void PrintBooks(IQueryable<string> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
    }
}
