using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.MountainsCodeFirst
{
    public class Country
    {
        public Country()
        {
            this.Mountains = new HashSet<Mountain>();
        }

        [Key]
        [StringLength(2, MinimumLength = 2)]
        [Column(TypeName = "char")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Mountain> Mountains { get; set; }
    }
}
