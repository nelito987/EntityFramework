using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.PhonebookCodeFirst.Models
{
    public class Channel
    {
        private ICollection<User> users;
        private ICollection<ChannelMessage> channelMesages;

        public Channel()
        {
            this.users = new HashSet<User>();
            this.channelMesages = new HashSet<ChannelMessage>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<ChannelMessage> ChannelMessages
        {
            get { return this.channelMesages; }
            set { this.channelMesages = value; }
        }
    }
}
