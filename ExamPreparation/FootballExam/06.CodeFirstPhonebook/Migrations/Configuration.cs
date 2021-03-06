namespace _06.CodeFirstPhonebook.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_06.CodeFirstPhonebook.PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(_06.CodeFirstPhonebook.PhonebookContext context)
        {

            if (!context.Contacts.Any())
            {
                context.Contacts.Add(new Contact()
                {
                    Name = "Peter Ivanov",
                    Position = "CTO",
                    Company = "Smart Ideas",
                    Emails = new HashSet<Email>()
                {
                    new Email() { EmailAddress =  "peter@gmail.com"},
                    new Email() { EmailAddress =  "peter_ivanov@yahoo.com"}
                },
                    Phones = new HashSet<Phone>()
                {
                    new Phone() { PhoneNumber = "+359 2 22 22 22"},
                    new Phone() { PhoneNumber = "+359 88 77 88 99"}
                },
                    Url = "http://blog.peter.com",
                    Notes = "Friend from school"
                });

                var maria = new Contact()
                {
                    Name = "Maria",
                    Phones = new HashSet<Phone>()
                    {
                        new Phone() { PhoneNumber = "+359 22 33 44 55" }
                    }
                };
                context.Contacts.Add(maria);

                var angie = new Contact()
                {
                    Name = "Angie Stanton",
                    Emails = new HashSet<Email>()
                    {
                        new Email() { EmailAddress = "info@angiestanton.com" }
                    },
                    Url = "http://angiestanton.com"
                };
                context.Contacts.Add(angie);

                context.SaveChanges();
            }
        }
    }
}
