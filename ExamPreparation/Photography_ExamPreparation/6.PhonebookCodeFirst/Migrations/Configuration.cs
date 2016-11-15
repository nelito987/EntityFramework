namespace _6.PhonebookCodeFirst.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(PhonebookContext context)
        {
            AddUsers(context);

            AddChannels(context);

            AddChannelMessages(context);
        }

        private static void AddUsers(PhonebookContext context)
        {
            var users = new List<User>()
            {
                new User {Username = "VGeorgiev", FullName = "Vladimir Georgiev", PhoneNumber = "0894545454"},
                new User {Username = "Nakov", FullName = "Svetlin Nakov", PhoneNumber = "0897878787"},
                new User {Username = "Ache", FullName = "Angel Georgiev", PhoneNumber = "0897121212"},
                new User {Username = "Alex", FullName = "Alexandra Svilarova", PhoneNumber = "0894151417"},
                new User {Username = "Petya", FullName = "Petya Grozdarska", PhoneNumber = "0895464646"}
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        private static void AddChannels(PhonebookContext context)
        {
            var channels = new List<Channel>
            {
                new Channel {Name = "Malinki"},
                new Channel {Name = "SoftUni"},
                new Channel {Name = "Admins"},
                new Channel {Name = "Programmers"},
                new Channel {Name = "Geeks"},
            };

            foreach (var channel in channels)
            {
                context.Channels.Add(channel);
            }

            context.SaveChanges();
        }

        private void AddChannelMessages(PhonebookContext context)
        {
            var channelMalinki = context.Channels.FirstOrDefault(x => x.Name == "Malinki");
            var nakov = context.Users.FirstOrDefault(x => x.Username == "Nakov");
            var vlado = context.Users.FirstOrDefault(x => x.Username == "VGeorgiev");
            var petya = context.Users.FirstOrDefault(x => x.Username == "Petya");

            var channelMessages = new List<ChannelMessage>
            {
                new ChannelMessage { ChannelId = channelMalinki.Id, Content = "Hey dudes, are you ready for tonight?", DateAndTime = DateTime.Now, UserId = petya.Id },
                new ChannelMessage { ChannelId = channelMalinki.Id, Content = "Hey Petya, this is the SoftUni chat.", DateAndTime = DateTime.Now, UserId = vlado.Id },
                new ChannelMessage { ChannelId = channelMalinki.Id, Content = "Hahaha, we are ready!", DateAndTime = DateTime.Now, UserId = nakov.Id },
                new ChannelMessage { ChannelId = channelMalinki.Id, Content = "Oh my god. I mean for drinking some beer!", DateAndTime = DateTime.Now, UserId = petya.Id },
                new ChannelMessage { ChannelId = channelMalinki.Id, Content = "We are sure!", DateAndTime = DateTime.Now, UserId = vlado.Id },
            };

            foreach (var channelMessage in channelMessages)
            {
                context.ChannelMessages.Add(channelMessage);
            }

            context.SaveChanges();
        }
    }
}
