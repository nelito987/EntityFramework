using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefect.Models
{
    public class Star
    {
        public Star()
        {
            this.Planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? SolarSystemId { get; set; }

        [ForeignKey("SolarSystemId")]
        public virtual SolarSystem SolarSystem { get; set; }

        public virtual HashSet<Planet> Planets { get; set; }
    }
}
