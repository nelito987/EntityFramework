using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassDefect.Data;
using Newtonsoft.Json;

namespace _04.ExportToJson
{
    public class ExportToJson
    {
        public static void Main(string[] args)
        {
            var context = new MassDefectDB();
            ExportPlantesWichAreNotAnomalyOrigins(context);
            ExportPeopleWhichHaveNotBeenVictims(context);
            ExportTopAnomaly(context);
        }

        private static void ExportTopAnomaly(MassDefectDB context)
        {
            var topAnomaly = context.Anomalies
                .OrderByDescending(a => a.Victims.Count)
                .Select(a => new
                {
                    a.Id,
                    originPlanet = new
                    {
                        a.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        a.TeleportPlanet.Name
                    },
                    victimsCount = a.Victims.Count
                });

            var anomaliesAsJson = JsonConvert.SerializeObject(topAnomaly, Formatting.Indented);
            File.WriteAllText("anomalies.json", anomaliesAsJson);
        }

        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectDB context)
        {
            var peopleToExport = context.Persons
                .Where(p => !p.Anomalies.Any())
                .Select(p => new
                {
                    name = p.Name,
                    homePlanet = new
                    {
                        name = p.HomePlanet.Name
                    }
                });

            var personsAsJson = JsonConvert.SerializeObject(peopleToExport, Formatting.Indented);
            File.WriteAllText("people.json", personsAsJson);
        }

        private static void ExportPlantesWichAreNotAnomalyOrigins(MassDefectDB context)
        {
            var exportedPlanets = context.Planets
                .Where(p => p.OriginAnomalies.Count == 0) // !p.OriginAnomalies.Any()
                .Select(p => new
                {
                    name = p.Name
                });

            var planetsAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText("planets.json", planetsAsJson);
        }
    }
}
