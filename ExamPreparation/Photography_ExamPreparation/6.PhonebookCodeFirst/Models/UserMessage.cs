using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.PhonebookCodeFirst.Models
{
    public class UserMessage
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateAndTime { get; set; }

        public int RecipientUserId { get; set; }

        public virtual User RecipientUser { get; set; }

        public int SenderUserId { get; set; }

        public virtual User SenderUser { get; set; }
    }
}
