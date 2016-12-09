using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DossierSystem.Models
{
    public class City
    {
        public City()
        {
            this.Locations = new HashSet<Location>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
