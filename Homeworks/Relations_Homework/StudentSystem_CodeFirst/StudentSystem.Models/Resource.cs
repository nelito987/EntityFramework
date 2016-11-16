using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ResourceType Type { get; set; }

        public string Url { get; set; }

        public virtual Course Course { get; set; }

        //public virtual ICollection<Licence> Licences { get; set; }
    }
}
