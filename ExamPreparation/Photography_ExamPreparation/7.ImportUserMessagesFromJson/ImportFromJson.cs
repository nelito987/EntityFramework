using _6.PhonebookCodeFirst;
using _6.PhonebookCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace _7.ImportUserMessagesFromJson
{
    class ImportFromJson
    {
        static void Main(string[] args)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");

            var json = File.ReadAllText(@"..\..\messages.json", Encoding.GetEncoding(1252)); // set the encoding to read special characters (I’m)
            var jsSerializer = new JavaScriptSerializer();
            var parsedMessages = jsSerializer.Deserialize<IEnumerable<MessageDto>>(json);

            foreach (var messageDto in parsedMessages)
            {
                try
                {
                    ImportMessageToDatabase(messageDto);
                    Console.WriteLine($"Message \"{messageDto.Content}\" imported");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private static void ImportMessageToDatabase(MessageDto messageDto)
        {
            if(string.IsNullOrWhiteSpace(messageDto.Content)) 
            {
                throw new ArgumentException("Content is required");
            }
            if (messageDto.DateTime == null)
            {
                throw new ArgumentException("DateTime is required");
            }
            if (messageDto.Recipient == null)
            {
                throw new ArgumentException("Recipient is required");
            }
            if (messageDto.Sender == null)
            {
                throw new ArgumentException("Sender is required");
            }

            var context = new PhonebookContext();

            var recipient = context.Users
                .Where(u => u.Username == messageDto.Recipient)
                .FirstOrDefault(); //extracting all info about the recipient 

            var senderId = context.Users
                .Where(u => u.Username == messageDto.Sender)
                .Select(u => u.Id) //extracting only the neededd info (id) - CORRECT!!!
                .FirstOrDefault(); 

            var newMessage = new UserMessage()
            {
                Content = messageDto.Content,
                DateAndTime = messageDto.DateTime,
                RecipientUserId = recipient.Id,
                SenderUserId = senderId
            };

            context.UserMessages.Add(newMessage);
            context.SaveChanges();         
        }
    }
}
