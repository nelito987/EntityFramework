using Photography_ExamPreparation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Runtime.CompilerServices;
using System.Data.Entity;

namespace _5.GenerateRandomEquipment
{
    class GenerateRandomEquipment
    {
        const int DefaultCount = 10;
        const string DefaultManufacturer = "Nikon";

        static void Main(string[] args)
        {    
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            int requestCount = 0;
            var inputXml = XDocument.Load("../../generate-equipments.xml");
            var xElementsGenerateRequests = inputXml.XPathSelectElements("/generate-random-equipments/generate");

            foreach(var xRequest in xElementsGenerateRequests)
            {
                Console.WriteLine("Processing request #{0} ...", ++requestCount);
                var request = ParseRequest(xRequest);
                ProcessRequest(request);
                Console.WriteLine();
            }
        }        

        private static Request ParseRequest(XElement xRequest)
        {
            var request = new Request();

            request.GenerateCount = DefaultCount;
            var xAttributeCount = xRequest.Attribute("generate-count");
            if(xAttributeCount != null)
            {
                request.GenerateCount = int.Parse(xAttributeCount.Value);
            }

            request.ManufacturerName = DefaultManufacturer;
            var xElementManufacturer = xRequest.Element("manufacturer");
            if (xElementManufacturer != null)
            {
                request.ManufacturerName = xElementManufacturer.Value;
            }

            return request;
        }

        private static void ProcessRequest(Request request)
        {
            var context = new PhotographyEntities();
            var manufacturersQuery = context.Manufacturers.AsQueryable();

            var manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == request.ManufacturerName);

            var cameraIds = context.Cameras
                .Where(c => c.ManufacturerId == manufacturer.Id)
                .Select(c => c.Id)
                .ToList();

            var lensIds = context.Lenses
                .Where(l => l.ManufacturerId == manufacturer.Id)
                .Select(l => l.Id)
                .ToList();

            var random = new Random();
            for (int i = 0; i < request.GenerateCount; i++)
            {
                var equipment = new Equipment();
                equipment.CameraId = cameraIds[random.Next(cameraIds.Count)];
                equipment.LensId = lensIds[random.Next(lensIds.Count)];

                context.Equipments.Add(equipment);
                context.SaveChanges();

                var equipmentDb = context.Equipments
                    .Include(x => x.Camera)
                    .Include(x => x.Lens)
                    .FirstOrDefault(x => x.Id == equipment.Id);

                Console.WriteLine($"Equipment added: {manufacturer.Name} (Camera: {equipmentDb.Camera.Model} - Lens: {equipmentDb.Lens.Model})");
            }
        }
    }
}
