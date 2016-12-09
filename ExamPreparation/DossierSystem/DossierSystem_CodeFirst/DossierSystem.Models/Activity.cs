using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierSystem.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
        
        public int ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }

        [Required]
        [ForeignKey("Individual")]
        public string IndividualId { get; set; }
        
        public virtual Individual Individual { get; set; }

        [Required]
        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }        
    }
}
