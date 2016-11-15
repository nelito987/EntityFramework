using Photography_ExamPreparation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace _4.ImportManufacturersAndLensesFromXml
{
    class ImportManufacturersFromXml
    {
        static void Main(string[] args)
        {
            var inputXml = XDocument.Load("../../manufacturers-and-lenses.xml");
            var xManufacturers = inputXml.XPathSelectElements("/manufacturers-and-lenses/manufacturer");
            var context = new PhotographyEntities();
            int manufacturerCount = 1;

            foreach (var xManufacturer in xManufacturers)
            {
                Console.WriteLine($"Processing manufacturer #{manufacturerCount}", ++manufacturerCount);
                Manufacturer manufacturer = CreateManufacturerIfNotExist(context, xManufacturer);
                var xLenses = xManufacturer.XPathSelectElements("lenses/lens");
                CreateLensIfNotExists(context, xLenses, manufacturer);
                Console.WriteLine();
            }
        }

        private static Manufacturer CreateManufacturerIfNotExist(PhotographyEntities context, XElement xManufacturer)
        {
            Manufacturer manufacturer = null;
            var xElementManufacturerName = xManufacturer.Element("manufacturer-name");
            if(xElementManufacturerName != null)
            {
                string manufacturerName = xElementManufacturerName.Value;
                manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == manufacturerName);
                if(manufacturer != null)
                {
                    Console.WriteLine($"Existing manufacturer: {manufacturerName}");
                }
                else
                {
                    // Create a new manufacturer in the database
                    manufacturer = new Manufacturer()
                    {
                        Name = manufacturerName
                    };
                    context.Manufacturers.Add(manufacturer);
                    context.SaveChanges();
                    Console.WriteLine($"Created manufacturer: {manufacturerName}");
                }
            }
            return manufacturer;
        }

        private static void CreateLensIfNotExists(
            PhotographyEntities context, 
            IEnumerable<XElement> xLenses, 
            Manufacturer manufacturer)
        {
            foreach(var xLense in xLenses)
            {
                // Find the lens by model and type (if exists)
                var lenseModel = xLense.Attribute("model").Value;
                var lenseType = xLense.Attribute("type").Value;
                var lensePrice = xLense.Attribute("price");

                var lens = context.Lenses.FirstOrDefault(l => l.Model == lenseModel);
                if(lens != null)
                {
                    Console.WriteLine($"Existing lens: {lenseModel}");
                }
                else
                {
                    lens = new Lens
                    {
                        Model = lenseModel,
                        Type = lenseType,
                        Price = lensePrice != null ? decimal.Parse(lensePrice.Value) : default(decimal?),
                        ManufacturerId = manufacturer.Id
                    };

                    context.Lenses.Add(lens);
                    context.SaveChanges();
                    Console.WriteLine("Created lens: {0}", lenseModel);
                }
            }
        }
    }
}
