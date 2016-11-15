using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.PhonebookCodeFirst.Models
{
    public class User
    {
        private ICollection<Channel> channels;
        private ICollection<UserMessage> sentUserMessages;
        private ICollection<UserMessage> recievedUserMessages;

        public User()
        {
            this.channels = new HashSet<Channel>();
            this.sentUserMessages = new HashSet<UserMessage>();
            this.recievedUserMessages = new HashSet<UserMessage>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<UserMessage> SentUserMessages
        {
            get { return this.sentUserMessages; }
            set { this.sentUserMessages = value; }
        }

        public virtual ICollection<UserMessage> RecievedUserMessages
        {
            get { return this.recievedUserMessages; }
            set { this.recievedUserMessages = value; }
        }

        public virtual ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
        }
    }
}
