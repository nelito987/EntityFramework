using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.PhonebookCodeFirst
{
    class PhonebookMain
    {
        static void Main(string[] args)
        {
            var context = new PhonebookContext();
            var channels = context.Channels
                .Select(c => new
                {
                    c.Name,
                    chanelMessages = c.ChannelMessages.Select(cm => new
                    {
                        cm.Content,
                        cm.DateAndTime,
                        cm.User.Username
                    })
                });

            foreach (var channel in channels)
            {
                Console.WriteLine(channel.Name);
                Console.WriteLine("-- Messages: --");
                foreach (var channelMessage in channel.chanelMessages)
                {
                    Console.WriteLine("Content: {0}, DateTime: {1}, User: {2}", channelMessage.Content, channelMessage.DateAndTime, channelMessage.Username);
                }
                Console.WriteLine();
            }
        }
    }
}
