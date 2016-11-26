using MassDefect.Data;
using MassDefect.Data.Utilities;
using MassDefect.Models;
using MassDefect.Models.ModelsDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _02.ImportDataFromJSON
{
    class ImportFromJSON
    {
        private const string SolarSystemPath = "../datasets/solar-systems.json";
        private const string StarsPath = "../datasets/stars.json";
        private const string PlanetsPath = "../datasets/planets.json";
        private const string PersonsPath = "../datasets/persons.json";
        private const string AnomaliesPath = "../datasets/anomalies.json";
        private const string AnomalyVictimsPath = "../datasets/anomaly-victims.json";


        static void Main(string[] args)
        {
            //ImportSolarSystems();
            //ImportStars();
            //ImportPlanets();
            //ImportPersons();
            //ImportAnomalies();
            ImportAnomalyVictims();
        }

        private static void ImportAnomalyVictims()
        {
            var context = new MassDefectDB();
            var json = File.ReadAllText(AnomalyVictimsPath);
            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomayVictimsDTO>>(json);

            foreach (var av in anomalyVictims)
            {
                if (av.Id == null || av.Person == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }


                var anomalyEntity = context.Anomalies.FirstOrDefault(a => a.Id == av.Id.Value);
                var personEntity = context.Persons.FirstOrDefault(p => p.Name == av.Person);


                if (anomalyEntity == null || personEntity == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                anomalyEntity.Victims.Add(personEntity);
                Console.WriteLine("success!!!");
            }

            context.SaveChanges();
        }

        private static void ImportAnomalies()
        {
            var context = new MassDefectDB();
            var json = File.ReadAllText(AnomaliesPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);

            foreach (var anomaly in anomalies)
            {
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                // Neno
                //var anomalyEntity = new Anomaly();
                //var an = context.Anomalies.FirstOrDefault(a => a.OriginPlanet.Name == anomaly.OriginPlanet);

                //if (an != null)
                //{
                //    anomalyEntity.OriginPlanet = an.OriginPlanet;
                //    anomalyEntity.TeleportPlanet = an.TeleportPlanet;
                //}         

                var anomalyEntity = new Anomaly();

                var originAnomaly = context.Anomalies.FirstOrDefault(a => a.OriginPlanet.Name == anomaly.OriginPlanet);
                var teleportPlanetAnomaly = context.Anomalies.FirstOrDefault(t => t.TeleportPlanet.Name == anomaly.TeleportPlanet);

                if (originAnomaly != null)
                {
                    anomalyEntity.OriginPlanet = originAnomaly.OriginPlanet;
                }

                if (teleportPlanetAnomaly != null)
                {
                    anomalyEntity.TeleportPlanet = teleportPlanetAnomaly.TeleportPlanet;
                }

                if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Anomalies.Add(anomalyEntity);
                Console.WriteLine(Constants.ImportUnnamedEntitySuccessMessage);
            }

            context.SaveChanges();
        }

        private static void ImportPersons()
        {
            var context = new MassDefectDB();
            var json = File.ReadAllText(PersonsPath);
            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);

            foreach (var person in persons)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var personEntity = new Person
                {
                    Name = person.Name,                    
                    HomePlanet = context.Planets.FirstOrDefault(p => p.Name == person.HomePlanet)
                };

                if (personEntity.HomePlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Persons.Add(personEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Person", personEntity.Name);
            }

            context.SaveChanges();
        }

        private static void ImportPlanets()
        {
            var context = new MassDefectDB();
            var json = File.ReadAllText(PlanetsPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

            foreach (var planet in planets)
            {
                if (planet.Name == null || planet.SolarSystem == null || planet.Sun == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var planetEntity = new Planet
                {
                    Name = planet.Name,
                    Sun = context.Stars.FirstOrDefault(s => s.Name == planet.Sun),
                    SolarSystem = context.SolarSystems.FirstOrDefault(ss => ss.Name == planet.SolarSystem)
                };

                 if (planetEntity.Sun == null || planetEntity.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Planets.Add(planetEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Planets", planetEntity.Name);
            }

            context.SaveChanges();            

        }

        /// Softuni solution
        //private static Star GetStarByName(string starName, MassDefectContext context)
        //{
        //    foreach (var star in context.Stars)
        //    {
        //        if (star.Name == starName)
        //        {
        //            return star;
        //        }
        //    }

        //    return null;
        //}

        private static void ImportStars()
        {
            var context = new MassDefectDB();
            var json = File.ReadAllText(StarsPath);
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

            foreach (var star in stars)
            {
                if(star.Name == null || star.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var starEntity = new Star
                {
                    Name = star.Name,
                    SolarSystem = context.SolarSystems.FirstOrDefault(s => s.Name == star.Name)
                };

                if(star.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Stars.Add(starEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Star", starEntity.Name);
            }

            context.SaveChanges();
        }

        private static void ImportSolarSystems()
        {
            var context = new MassDefectDB();
            var json = File.ReadAllText(SolarSystemPath);
            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);

            foreach (var solarSystem in solarSystems)
            {
                if(solarSystem.Name == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var solarSystemEntity = new SolarSystem
                {
                    Name = solarSystem.Name
                };

                context.SolarSystems.Add(solarSystemEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Solar System", solarSystemEntity.Name);
            }
            context.SaveChanges();
        }
    }
}
