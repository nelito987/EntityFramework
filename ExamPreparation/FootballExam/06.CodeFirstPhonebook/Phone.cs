using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.CodeFirstPhonebook
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
