using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookShopsystemLab.Data;

namespace BookShopSystemLab.ConsoleClient
{
    public class BookShopMain
    {
        private static void Main(string[] args)
        {
            var context = new BookShopLabContext();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");
            //var count = context.Books.Count();

            // 1.	Get all books after the year 2000. Print only their titles.
            // GetAllBooksAfter2000(context);

            // 2.	Get all authors with at least one book with release date before 1990.Print their first name and last name.
            //GetAllAuthorsBooksBefore1990(context);

            // 3.	Get all authors, ordered by the number of their books (descending). Print their first name, last name and book count.
            // AuthorsOrderedByBooksQty(context);

            // 4.	Get all books from author George Powell, ordered by their release date (descending), 
            // then by book title (ascending). Print the book's title, release date and copies.
            //AllGeorgePowellBooks(context);

            //5.	**Get the most recent books by categories. 
            // The categories should be ordered by total book count. Only take the top 3 most recent books from each category
            // - ordered by date (descending), then by title (ascending). Select the category name, total book count and for each book 
            // - its title and release date.
            MostRecentBooksByCategory(context);
        }

        private static void GetAllBooksAfter2000(BookShopLabContext context)
        {
            var allBooksAfter2000 = context.Books
                .Where(b => b.ReleaseDate.Value.Year > 2000)
                .Select(b => b.Title);
            foreach (var title in allBooksAfter2000)
            {
                Console.WriteLine(title);
            }
        }

        private static void GetAllAuthorsBooksBefore1990(BookShopLabContext context)
        {
            var authors = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 1990).Select(b => new
                {
                    b.Author.FirstName,
                    b.Author.LastName
                })
                .Distinct();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }
        }

        private static void AuthorsOrderedByBooksQty(BookShopLabContext context)
        {
            var authors = context.Authors
                .OrderByDescending(a => a.Books.Count)
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    booksCount = a.Books.Count
                });
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}, books: {author.booksCount}");
            }
        }

        private static void AllGeorgePowellBooks(BookShopLabContext context)
        {
            var books = context.Books
                .Where(b => b.Author.FirstName == "George" && b.Author.LastName == "Powell")
                .OrderByDescending(b => b.ReleaseDate)
                .ThenBy(b => b.Title)
                .Select(b => new
                {
                    b.Title,
                    b.ReleaseDate,
                    b.Copies
                });

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} {book.ReleaseDate} {book.Copies}");
            }
        }

        private static void MostRecentBooksByCategory(BookShopLabContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    c.Books,
                    BooksCount = c.Books.Count
                })
                .OrderByDescending(c => c.BooksCount);

            foreach (var category in categories)
            {
                Console.WriteLine($"---{category.Name}: {category.BooksCount}");

                var books = category.Books
                    .OrderByDescending(b => b.ReleaseDate)
                    .ThenBy(b => b.Title)
                    .Take(3)
                    .Select(b => new
                    {
                        b.Title,
                        b.ReleaseDate
                    });

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }
        }
    }
}
