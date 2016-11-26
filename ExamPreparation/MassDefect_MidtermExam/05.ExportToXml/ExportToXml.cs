using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MassDefect.Data;

namespace _05.ExportToXml
{
    public class ExportToXml
    {
        public static void Main(string[] args)
        {
            var context = new MassDefectDB();
            var exportedAnomalies = context.Anomalies
                .Select(a => new
                {
                    id = a.Id,
                    originPlanet = a.OriginPlanet.Name,
                    teleportPlanet = a.TeleportPlanet.Name,
                    victims = a.Victims.Select(v => v.Name)
                })
                .OrderBy(a => a.id);

            var xmlDocument = new XElement("anomalies");

            foreach (var exportedAnomaly in exportedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", exportedAnomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", exportedAnomaly.originPlanet));
                anomalyNode.Add(new XAttribute("teleport-planet", exportedAnomaly.teleportPlanet));

                var victimsNode = new XElement("victims");
                foreach (var v in exportedAnomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", v));
                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save("../../anomalies.xml");
        }
    }
}
