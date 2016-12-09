using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photography.DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PhotographySystemEntities();
            var photographs = context.Photographs
                .Select(p => new
                {
                    p.Title,
                    p.Link
                });

            foreach (var photo in photographs)
            {
                Console.WriteLine($"{photo.Title} -- {photo.Link}");
            }
        }
    }
}
