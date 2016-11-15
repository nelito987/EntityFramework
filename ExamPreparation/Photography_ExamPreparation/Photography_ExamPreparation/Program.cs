using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photography_ExamPreparation
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PhotographyEntities();
            // Neli's solution
            //var allCameras = context.Cameras
            //    .Select(c => new
            //    {
            //        ManufacturerName = c.Manufacturer.Name
            //        + " " +
            //        c.Model
            //    }).OrderBy(c => c.ManufacturerName);

            //foreach(var c in allCameras)
            //{
            //    Console.WriteLine(c.ManufacturerName);
            //}

            // Author's solution
            var allCameras = context.Cameras
                .Select(c => c.Manufacturer.Name + " " + c.Model)
                .OrderBy(c => c);

            Console.WriteLine(string.Join("\n", allCameras));
        }
    }
}
