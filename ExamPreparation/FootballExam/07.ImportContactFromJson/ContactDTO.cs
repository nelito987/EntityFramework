using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.ImportContactFromJson
{
    class ContactDTO
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public string Site { get; set; }

        public string Notes { get; set; }

        public virtual string[] Emails { get; set; }

        public virtual string[] Phones { get; set; }
    }
}
