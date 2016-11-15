using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.PhonebookCodeFirst.Models
{
    public class ChannelMessage
    {
        public int Id { get; set; }

        public string Content { get; set; }
        
        public DateTime DateAndTime { get; set; }

        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
