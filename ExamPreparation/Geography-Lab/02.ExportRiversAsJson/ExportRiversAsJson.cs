using _01.EF_Mappings;
using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace _02.ExportRiversAsJson
{
    class ExportRiversAsJson
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();

            var rivers = context.Rivers
                .OrderByDescending(r => r.Length)
                .Select(r => new
            {
                riverName = r.RiverName,
                riverLength = r.Length,
                countries = r.Countries
                    .OrderBy(c => c.CountryName)
                    .Select(c => c.CountryName)                
            });

            var jsSerializer = new JavaScriptSerializer();
            var riversJson = jsSerializer.Serialize(rivers.ToList());
            Console.WriteLine(riversJson);
            File.WriteAllText("rivers.json", riversJson);


            //printing rivers on the console
            foreach (var r in rivers)
            {
                Console.WriteLine("River name: {0}, lenght: {1}, Countries: {2}",
                    r.riverName, r.riverLength, String.Join(", ", r.countries));
            }
        }
    }
}
