using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierSystem.Models
{
    public class Individual
    {
        public Individual()
        {
            this.RelatedIndividuals = new HashSet<Individual>();
            this.Locations = new HashSet<Location>();
            this.Activities = new HashSet<Activity>();
        }

        [Key]        
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Alias { get; set; }

        public DateTime? BirthDate { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<Individual> RelatedIndividuals { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
