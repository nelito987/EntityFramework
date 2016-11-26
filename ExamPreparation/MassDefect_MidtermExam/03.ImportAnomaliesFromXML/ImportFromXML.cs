using MassDefect.Data;
using MassDefect.Data.Utilities;
using MassDefect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace _03.ImportAnomaliesFromXML
{
    public class ImportFromXML
    {
        private const string NewAnomaliesPath = "../../datasets/new-anomalies.xml";

        public static void Main(string[] args)
        {
            var xml = XDocument.Load(NewAnomaliesPath);
            var anomalyNodes = xml.XPathSelectElements("anomalies/anomaly");
            var context = new MassDefectDB();
            var anomalyEntity = new Anomaly();

            foreach (var anomalyNode in anomalyNodes)
            {
                var originPlanetName = anomalyNode.Attribute("origin-planet");
                var teleportPlanetName = anomalyNode.Attribute("teleport-planet");

                if (originPlanetName == null || teleportPlanetName == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }
                else
                {
                    anomalyEntity.OriginPlanet = context.Planets.FirstOrDefault(p => p.Name == originPlanetName.Value);
                    anomalyEntity.TeleportPlanet = context.Planets.FirstOrDefault(tp => tp.Name == teleportPlanetName.Value);
                }

                //var anomalyEntity = new Anomaly
                //{
                //    OriginPlanet = context.Planets.FirstOrDefault(p => p.Name == originPlanetName.Value),
                //    TeleportPlanet = context.Planets.FirstOrDefault(tp => tp.Name == teleportPlanetName.Value)
                //};

                if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Anomalies.Add(anomalyEntity);
                Console.WriteLine(Constants.ImportUnnamedEntitySuccessMessage);

                var victimsNodes = anomalyNode.XPathSelectElements("victims/victim");
                foreach (var victimNode in victimsNodes)
                {
                    ImportVictims(victimNode, context, anomalyEntity);
                }

                context.SaveChanges();
            }
        }

        private static void ImportVictims(XElement victimNode, MassDefectDB context, Anomaly anomaly)
        {
            var victimNameAttr = victimNode.Attribute("name");
            if (victimNameAttr == null)
            {
                Console.WriteLine(Constants.ImportErrorMessage);
                return;
            }

            var personEntity = context.Persons.FirstOrDefault(p => p.Name == victimNameAttr.Value);

            if (personEntity == null)
            {
                Console.WriteLine(Constants.ImportErrorMessage);
                return;
            }

            anomaly.Victims.Add(personEntity);
        }
    }
}
