using DossierSystem.Models;
using DossierSystem.Models.ModelsDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DossierSystem.Data
{
    class DossierDataMain
    {
        private const string CityPath = "../../data/cities.xml";
        private const string IndividualsPath = "../../data/individuals.json";
        private const string ActivityTypesPath = "../../data/activity-types.json";

        private static IDictionary<string, int> activityTypeIds;
        private static IDictionary<string, int> citiesIds;
        
        static void Main(string[] args)
        {
            var context = new DossierContext();
            SeedDatabase();
        }

        private static void SeedDatabase()
        {
            ImportCities();
            ImportActivityTypes();
            InitializeDictionaries();
            ImportIndividuals();
        }

        private static void InitializeDictionaries()
        {
            var context = new DossierContext();
            activityTypeIds = new Dictionary<string, int>();
            citiesIds = new Dictionary<string, int>();

            context.ActivityTypes
                .Select(at => new { at.Id, at.FullName })
                .ToList()
                .ForEach(el => activityTypeIds[el.FullName] = el.Id);

            context.Cities
                .Select(c => new { c.Id, c.Name })
                .ToList()
                .ForEach(c => citiesIds[c.Name] = c.Id);
        }

        private static void ImportIndividuals()
        {
            //Softuni solution
            var context = new DossierContext();
            var json = File.ReadAllText(IndividualsPath);
            var individuals = JsonConvert.DeserializeObject<IEnumerable<IndividualDTO>>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            foreach (var individual in individuals)
            {
                var exists = context.Individuals.Any(i => i.Id == individual.Id);
                if (exists)
                {
                    Console.WriteLine("{0} is already present in the database.",
                        individual.FullName);
                    continue;
                }

                var individualEntity = new Individual
                {
                    Id = individual.Id,
                    FullName = individual.FullName,
                    Alias = individual.Alias,
                    Status = individual.Status,
                };

                if (individual.BirthDate.HasValue)
                {
                    individualEntity.BirthDate = individual.BirthDate.Value;
                }

                foreach (var activityDto in individual.Activities)
                {
                    var activity = new Activity
                    {
                        Description = activityDto.Description,
                        ActiveFrom = activityDto.ActiveFrom,
                        ActivityTypeId = activityTypeIds[activityDto.ActivityType],
                        IndividualId = individualEntity.Id
                    };

                    if (activityDto.ActiveTo.HasValue)
                    {
                        activity.ActiveTo = activityDto.ActiveTo.Value;
                    }

                    individualEntity.Activities.Add(activity);
                }

                foreach (var locationDto in individual.Locations)
                {
                    var location = new Location
                    {
                        CityId = citiesIds[locationDto.City],
                        LastSeen = locationDto.LastSeen,
                        IndividualId = individualEntity.Id
                    };

                    individualEntity.Locations.Add(location);
                }

                context.Individuals.Add(individualEntity);
                context.SaveChanges();
                Console.WriteLine("Successfully imported data for {0}",
                    individual.FullName);
            }
        }

        private static void ImportActivityTypes()
        {
            var context = new DossierContext();
            var json = File.ReadAllText(ActivityTypesPath);
            var activityTypes = JsonConvert.DeserializeObject<IEnumerable<ActivityTypeDTO>>(json);
            foreach (var at in activityTypes)
            {
                var newAt = new ActivityType() 
                { 
                    FullName = at.Name
                };
                context.ActivityTypes.Add(newAt);
            }

            context.SaveChanges();
        }

        private static void ImportCities()
        {
            var context = new DossierContext();
            var xml = XDocument.Load(CityPath);
            var cities = xml.XPathSelectElements("cities/city");

            foreach (var city in cities)
            {
                var cityName = city.Attribute("name").Value;
                context.Cities.Add(new City() { Name = cityName });
            }

            context.SaveChanges();
        }
    }
}
