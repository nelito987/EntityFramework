using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.MountainsCodeFirst
{
    public class Mountain
    {
        public Mountain()
        {
            this.Peaks = new HashSet<Peak>();
            this.Countries = new HashSet<Country>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Peak> Peaks { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}
