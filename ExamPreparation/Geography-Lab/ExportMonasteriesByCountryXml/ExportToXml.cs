using _01.EF_Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _03.ExportMonasteriesByCountryXml
{
    class ExportToXml
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();

            var countries = context.Countries
                .Where(c => c.Monasteries.Any())
                .OrderBy(c => c.CountryName)
                .Select(c => new
                {
                    CountryName = c.CountryName,
                    Monasteries = c.Monasteries
                        .OrderBy(m => m.Name)
                        .Select(m => m.Name)
                }).ToList();

            foreach (var c in countries)
            {
                Console.WriteLine("Country name: {0}, monasteries: {1}",
                    c.CountryName, String.Join(", ", c.Monasteries));
            }

            // building the XML
            var xmlMonasteries = new XElement("monasteries");
            foreach(var c in countries)
            {
                var xmlCountry = new XElement("country");
                xmlCountry.Add(new XAttribute("name", c.CountryName));
                xmlMonasteries.Add(xmlCountry);

                foreach(var m in c.Monasteries)
                {
                    xmlCountry.Add(new XElement("monastery", m));
                }
            }

            var xmlDoc = new XDocument(xmlMonasteries);
            xmlDoc.Save("monasteries.xml");
        }
    }
}
