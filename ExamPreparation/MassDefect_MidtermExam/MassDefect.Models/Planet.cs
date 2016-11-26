using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDefect.Models
{
    public class Planet
    {
        public Planet()
        {
            this.PersonsOnPlanet = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SolarSystemId { get; set; }

        [ForeignKey("SolarSystemId")]
        public virtual SolarSystem SolarSystem { get; set; }

        // Sun ???
        public int SunId { get; set; }

        [ForeignKey("SunId")]
        public virtual Star Sun { get; set; }        

        public virtual ICollection<Person> PersonsOnPlanet { get; set; }

        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }

        public virtual ICollection<Anomaly> TeleportAnomalies { get; set; }

    }
}
