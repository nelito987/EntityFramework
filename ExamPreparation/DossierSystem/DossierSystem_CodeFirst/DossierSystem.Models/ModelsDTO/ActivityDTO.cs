using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierSystem.Models.ModelsDTO
{
    public class ActivityDTO
    {
        public string ActivityType { get; set; }

        public string Description { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }
    }
}
