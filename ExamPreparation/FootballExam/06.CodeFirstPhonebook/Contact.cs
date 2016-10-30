using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.CodeFirstPhonebook
{
    public class Contact
    {
        public Contact()
        {
            this.Emails = new HashSet<Email>();
            this.Phones = new HashSet<Phone>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Position { get; set; }

        [StringLength(200)]
        public string Company { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public string Notes { get; set; }
        

        public virtual ICollection<Email> Emails { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
