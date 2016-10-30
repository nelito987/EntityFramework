using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.EF_Mappings
{
    class ListContitnents
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();
            foreach(var c in context.Continents)
            {
                Console.WriteLine(c.ContinentName);
            }
        }
    }
}
