using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DossierSystem.Models
{
    public class Location
    {
        
        //public int Id { get; set; }

        //[ForeignKey("City")]
        //public int CityId { get; set; }
        
        //public virtual City City { get; set; }

        //[Required]
        //[ForeignKey("Individual")]
        //public string IndividualId { get; set; }
        
        //public virtual Individual Individual { get; set; }

        //public DateTime LastSeenOn { get; set; }

        public int Id { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string IndividualId { get; set; }

        public virtual Individual Individual { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
