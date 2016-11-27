using System.Globalization;
using System.IO;
using BookShopSystemsLab.Models;

namespace BookShopsystemLab.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopLabContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "BookShopsystemLab.Data.BookShopLabContext";
        }

        protected override void Seed(BookShopLabContext context)
        {
            if (!context.Authors.Any())
            {
                this.AddAuthorsToDatabase(context);
            }

            if (!context.Categories.Any())
            {
                this.AddCategoriesToDatabase(context);
            }

            if (!context.Books.Any())
            {
                this.AddBooksToDatabase(context);
                this.RandomlyAddCategoriesToBook(context);
            }
            
        }

        private void RandomlyAddCategoriesToBook(BookShopLabContext context)
        {
            Random random = new Random();
            for (int i = 0; i < context.Books.Count(); i++)
            {
                int categoriesPerBook = random.Next(1, 5);
                for (int j = 0; j < categoriesPerBook; j++)
                {
                    int categoryId = random.Next(1, 9);
                    if (!context.Books.Find(i + 1).Categories.Contains(context.Categories.Find(categoryId)))
                    {
                        context.Books.Find(i + 1).Categories.Add(context.Categories.Find(categoryId));
                        context.Categories.Find(categoryId).Books.Add(context.Books.Find(i + 1));
                    }
                }
            }
        }

        private void AddBooksToDatabase(BookShopLabContext context)
        {
            using (var reader = new StreamReader("../../../SeedData/books.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var bookData = line.Split(new[] { ' ' }, 6);

                    var editionType = (EditionType)int.Parse(bookData[0]);
                    var releaseDate = DateTime.ParseExact(bookData[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var copies = int.Parse(bookData[2]);
                    var price = decimal.Parse(bookData[3]);
                    var ageRestriction = (AgeRestriction)int.Parse(bookData[4]);
                    var title = bookData[5];

                    Random random = new Random();
                    var authors = context.Authors.ToList();
                    var authorIndex = random.Next(1, authors.Count());
                    var authorId = authors[authorIndex].Id;

                    context.Books.Add(new Book()
                    {
                        Title = title,
                        EditionType = editionType,
                        Price = price,
                        Copies = copies,
                        ReleaseDate = releaseDate,
                        AuthorId = authorId,
                        AgeRestriction = ageRestriction
                    });

                    //context.SaveChanges();
                    line = reader.ReadLine();
                }
                context.SaveChanges();
            }
        }

        private void AddCategoriesToDatabase(BookShopLabContext context)
        {
            using (var reader = new StreamReader("../../../SeedData/categories.txt"))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    if (line != "")
                    {
                        context.Categories.Add(new Category()
                        {
                            Name = line
                        });
                    }
                    //context.SaveChanges();
                    line = reader.ReadLine();
                }
            }
            context.SaveChanges();
        }

        private void AddAuthorsToDatabase(BookShopLabContext context)
        {
            using (var reader = new StreamReader("../../../SeedData/authors.txt"))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    var authorData = line.Split(new[] { ' ' }, 2);
                    var authorFirstName = authorData[0];
                    var authorLastName = authorData[1];

                    context.Authors.Add(new Author()
                    {
                        FirstName = authorFirstName,
                        LastName = authorLastName
                    });
                    
                    line = reader.ReadLine();
                }
            }
            context.SaveChanges();
        }
    }
}
