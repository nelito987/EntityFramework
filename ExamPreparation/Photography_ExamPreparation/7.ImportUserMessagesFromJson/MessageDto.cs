using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7.ImportUserMessagesFromJson
{
    public class MessageDto
    {
        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public string Recipient { get; set; }

        public string Sender { get; set; }

    }
}
