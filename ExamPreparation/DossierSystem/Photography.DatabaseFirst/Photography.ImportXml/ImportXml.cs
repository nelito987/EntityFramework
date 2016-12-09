using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Photography.DatabaseFirst;

namespace Photography.ImportXml
{
    class ImportXml
    {
        private static string ManufacturerPath = "../../manufacturer.xml";
        static void Main(string[] args)
        {
            var xml = XDocument.Load(ManufacturerPath);
            var manufacturerNodes = xml.XPathSelectElements("manufacturers/manufacturer");
            var context = new PhotographySystemEntities();
            
            foreach (var manufacturerNode in manufacturerNodes)
            {
                var manufacturerEntity = new Manufacturer();
                var manufacturerName = manufacturerNode.Attribute("name").Value;

                if (context.Manufacturers.Any(m => m.Name == manufacturerName))
                {
                    Console.WriteLine("Manufacturer {0} already exists.",
                        manufacturerName);
                    continue;
                }
                manufacturerEntity.Name = manufacturerName;

                var cameras = manufacturerNode.XPathSelectElements("cameras/camera");
                ImportCameras(cameras, manufacturerEntity);

                var lenses = manufacturerNode.XPathSelectElements("lenses/lens");
                ImportLenses(lenses, manufacturerEntity);

                context.Manufacturers.Add(manufacturerEntity);
                context.SaveChanges();
                Console.WriteLine("Successfully added manufacturer {0}.",manufacturerName);
            }
        }

        private static void ImportLenses(IEnumerable<XElement> lenses, Manufacturer manufacturerEntity)
        {
            foreach (var lensNode in lenses)
            {
                var lens = new Lens();
                lens.Model = lensNode.Attribute("model").Value;
                lens.Type = lensNode.Attribute("type").Value;

                if (lensNode.Attribute("price") != null)
                {
                    lens.Price = decimal.Parse(lensNode.Attribute("price").Value);
                }

                manufacturerEntity.Lenses.Add(lens);
            }
        }

        private static void ImportCameras(IEnumerable<XElement> cameras, Manufacturer manufacturer)
        {
            foreach (var cameraNode in cameras)
            {
                var camera = new Camera();
                camera.Model = cameraNode.Attribute("model").Value;
                camera.Year = int.Parse(cameraNode.Attribute("year").Value);

                if (cameraNode.Attribute("megapixels") != null)
                {
                    camera.Megapixels = int.Parse(cameraNode.Attribute("megapixels").Value);
                }

                if (cameraNode.Attribute("price") != null)
                {
                    camera.Price = decimal.Parse(cameraNode.Attribute("price").Value);
                }

                manufacturer.Cameras.Add(camera);
            }
        }
    }
}
