using BookShopsystemLab.Data.Migrations;
using BookShopSystemsLab.Models;

namespace BookShopsystemLab.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BookShopLabContext : DbContext
    {
        public BookShopLabContext()
            : base("name=BookShopsystemLab.Data.BookShopLabContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookShopsystemLab.Data.BookShopLabContext, Configuration>());
        }

        public IDbSet<Book> Books { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.RelatedBooks)
                .WithMany()
                .Map(configuration =>
                {
                    configuration.MapLeftKey("BookId");
                    configuration.MapRightKey("RelatedBookId");
                    configuration.ToTable("BooksRelatedBooks");
                });

            base.OnModelCreating(modelBuilder);
        }

    }
    
}