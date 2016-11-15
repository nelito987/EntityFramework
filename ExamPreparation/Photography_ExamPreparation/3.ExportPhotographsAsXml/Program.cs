using Photography_ExamPreparation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3.ExportPhotographsAsXml
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PhotographyEntities();

            var photographs = context.Photographs
                .OrderBy(p => p.Title)
                .Select(p => new
                {
                    p.Title,
                    category = p.Category.Name,
                    p.Link,
                    Equipment = new
                    {
                        Camera = new
                        {
                            Name = p.Equipment.Camera.Manufacturer.Name + " " + p.Equipment.Camera.Model,
                            Megapixels = p.Equipment.Camera.Megapixels
                        },
                        Lens = new
                        {
                            Model = p.Equipment.Lens.Manufacturer.Name + " " + p.Equipment.Lens.Model,
                            Price = p.Equipment.Lens.Price
                        }
                    }                        
                });

            var resultXml = new XElement("photographs");

            foreach (var photo in photographs)
            {
                var photoXml = new XElement("photograph");
                photoXml.Add(new XAttribute("title", photo.Title));
                photoXml.Add(new XElement("category", photo.category));
                photoXml.Add(new XElement("link", photo.Link));

                var equipmentXml = new XElement("equipment");
                equipmentXml.Add(new XElement("camera", photo.Equipment.Camera.Name, new XAttribute("megapixels", photo.Equipment.Camera.Megapixels)));

                if (photo.Equipment.Lens.Price.HasValue)
                {
                    equipmentXml.Add(new XElement("lens", photo.Equipment.Lens.Model, new XAttribute("price", string.Format("{0:f2}", photo.Equipment.Lens.Price))));
                }
                else
                {
                    equipmentXml.Add(new XElement("lens", photo.Equipment.Lens.Model));
                }

                photoXml.Add(equipmentXml);
                resultXml.Add(photoXml);
            }

            var resultXmlDoc = new XDocument();
            resultXmlDoc.Add(resultXml);
            resultXmlDoc.Save("photographs.xml");

            Console.WriteLine("Photographs exported to photographs.xml");

        }
    }
}
