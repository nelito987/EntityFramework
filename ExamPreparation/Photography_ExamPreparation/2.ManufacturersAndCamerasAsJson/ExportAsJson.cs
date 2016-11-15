using Photography_ExamPreparation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace _2.ManufacturersAndCamerasAsJson
{
    public class ExportAsJson
    {
        static void Main(string[] args)
        {
            var context = new PhotographyEntities();
            var manufacturers = context.Manufacturers
                .OrderBy(m => m.Name)
                .Select(m => new
                {
                    m.Name,
                    cameras = m.Cameras
                        .OrderBy(c => c.Model)
                        .Select(c => new
                        {
                            c.Model,
                            c.Price
                        })
                }).ToList();
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(manufacturers);
            File.WriteAllText("manufactureres-and-cameras.json", json);
            Console.WriteLine("manufactureres-and-cameras.json exported");
        }
    }
}
