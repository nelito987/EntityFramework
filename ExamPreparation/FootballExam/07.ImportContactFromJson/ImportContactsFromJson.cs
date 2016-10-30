using _06.CodeFirstPhonebook;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace _07.ImportContactFromJson
{
    class ImportContactsFromJson
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("../../contacts.json");
            var jsonSerializer = new JavaScriptSerializer();
            var parsedContacts = jsonSerializer.Deserialize<ContactDTO[]>(text);

            foreach(var contactDTO in parsedContacts)
            {
                try
                {
                    if(contactDTO.Name == null)
                    {
                        throw new ArgumentNullException("Name is required.");
                    }

                    var newContact = new Contact()
                    {
                        Name = contactDTO.Name,
                        Company = contactDTO.Company,
                        Position = contactDTO.Position,
                        Url = contactDTO.Site,
                        Notes = contactDTO.Notes
                    };

                    if(contactDTO.Emails != null)
                    {
                        newContact.Emails = contactDTO.Emails
                            .Select(e => new Email() { EmailAddress = e }).ToList();
                    }

                    if(contactDTO.Phones != null)
                    {
                        newContact.Phones = contactDTO.Phones
                            .Select(p => new Phone() { PhoneNumber = p }).ToList();
                    }

                    var context = new PhonebookContext();
                    context.Contacts.Add(newContact);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }            
        }
    }
}
