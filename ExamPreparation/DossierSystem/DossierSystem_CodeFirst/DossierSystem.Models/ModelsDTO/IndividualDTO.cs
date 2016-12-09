using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierSystem.Models.ModelsDTO
{
    public class IndividualDTO
    {
        public string Id { get; set; }
        
        public string FullName { get; set; }

        public string Alias { get; set; }

        public DateTime? BirthDate { get; set; }

        public Status Status { get; set; }

        public IEnumerable<ActivityDTO> Activities { get; set; }

        public IEnumerable<LocationDTO> Locations { get; set; }
    }
}
