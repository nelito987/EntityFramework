using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.CodeFirstPhonebook
{
    class CodeFirstPhonebook
    {
        static void Main(string[] args)
        {
            var context = new PhonebookContext();

            var peoples = context.Contacts.Select(c => new
            {
                ContactName = c.Name,
                ContactPhone = c.Phones.Select(p => p.PhoneNumber),
                ContactEmails = c.Emails.Select(e => e.EmailAddress)
            }).ToList();

            foreach(var person in peoples)
            {
                Console.WriteLine(person.ContactName);
                Console.WriteLine("Emails: " + String.Join(", ", person.ContactEmails));
                Console.WriteLine("Phone numbers: " + String.Join(", ", person.ContactPhone));
                Console.WriteLine();
            }
        }
    }
}
