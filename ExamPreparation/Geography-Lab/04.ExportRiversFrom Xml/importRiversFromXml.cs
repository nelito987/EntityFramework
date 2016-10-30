using _01.EF_Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace _04.ImportRiversFrom_Xml
{
    class importRiversFromXml
    {
        static void Main(string[] args)
        {
            var xmlDoc = XDocument.Load(@"..\..\rivers.xml");
            var riverNodes = xmlDoc.XPathSelectElements("/rivers/river");
            var context = new GeographyEntities();

            foreach(var riverNode in riverNodes)
            {
                var river = new River();
                string riverName = riverNode.Element("name").Value;
                int riverLength = int.Parse(riverNode.Element("length").Value);
                string riverOutflow = riverNode.Element("outflow").Value;

                int? drainageArea = null;
                int? averageDischarge = null;
                
                var riverNodeElement = riverNode.Element("drainage-area");
                if (riverNodeElement != null)
                {
                    drainageArea = int.Parse(riverNodeElement.Value);
                }

                if(riverNode.Element("average-discharge") != null)
                {
                    averageDischarge = int.Parse(riverNode.Element("average-discharge").Value);
                }

                river.RiverName = riverName;
                river.Length = riverLength;
                river.Outflow = riverOutflow;
                river.DrainageArea = drainageArea;
                river.AverageDischarge = averageDischarge;                

                var countryNodes = riverNode.XPathSelectElements("countries/country");
                var countryNames = countryNodes.Select(c => c.Value);

                foreach(var countryName in countryNames)
                {
                    var country = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                    river.Countries.Add(country);
                }

                context.Rivers.Add(river);
                context.SaveChanges();
            }
        }
    }
}
