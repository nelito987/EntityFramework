using _01.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _03.ExportMatchesToXml
{
    class ExportIntMatchesToXml
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();

            var matches = context.InternationalMatches
                .OrderByDescending(m => m.MatchDate)
                .ThenBy(m => m.CountryHome.CountryName)
                .ThenBy(m => m.CountryAway.CountryName)
                .Select(m => new
                {
                    HomeScore = m.HomeGoals,
                    AwayScore = m.AwayGoals,
                    HomeCodeCountry = m.HomeCountryCode,
                    AwayCodeCountry = m.AwayCountryCode,
                    HomeCountry = m.CountryHome.CountryName,
                    AwayCountry = m.CountryAway.CountryName,
                    Date = m.MatchDate,
                    MatchLeagueName = m.League.LeagueName
                }).ToList();

            var resultXml = new XElement("matches");

            foreach(var match in matches)
            {
                var matchXml = new XElement("match");

                if(match.Date != null)
                {
                    if(match.Date.Value.TimeOfDay == TimeSpan.Zero)
                    {
                        string date = match.Date.Value.ToString("dd-MMM-yyyy");
                        matchXml.Add(new XAttribute("date", date));
                    }
                    else
                    {
                        string dateTime = match.Date.Value.ToString("dd-MMM-yyyy hh:mm");
                        matchXml.Add(new XAttribute("date-time", dateTime));
                    }
                }
                matchXml.Add(new XElement("home-country", match.HomeCountry, new XAttribute("code", match.HomeCodeCountry)));
                matchXml.Add(new XElement("away-country", match.AwayCountry, new XAttribute("code", match.AwayCodeCountry)));

                if(match.HomeScore != null && match.AwayScore != null)
                {
                    string score = match.HomeScore.Value + ":" + match.AwayScore.Value;
                }

                if(match.MatchLeagueName != null)
                {
                    matchXml.Add(new XElement("league", match.MatchLeagueName));
                }
                resultXml.Add(matchXml);
            }

            var resultDoc = new XDocument();
            resultDoc.Add(resultXml);
            resultDoc.Save("international-matches.xml");

            Console.WriteLine("Matches exported to international-matches.xml");
        }
    }
}
