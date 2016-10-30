using _01.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace _05.GenerateRandomMatches
{
    class RandomMatches
    {
        const int DefaultGenerateCount = 10;
        const int DefaultMaxGoals = 5;
        static readonly DateTime DefaultStartDate = new DateTime(2000, 1, 1);
        static readonly DateTime DefaultEndDate = new DateTime(2015, 12, 31);
        static void Main(string[] args)
        {
            int requestCount = 0;
            var inputXml = XDocument.Load("../../generate-matches.xml");
            var xElementsGenerateRequests = inputXml.XPathSelectElements("/generate-random-matches/generate"); // ???
            foreach (var xRequest in xElementsGenerateRequests)
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

            // count
            request.GenerateCount = DefaultGenerateCount;
            var xAttributeCount = xRequest.Attribute("generate-count");
            if(xAttributeCount != null)
            {
                request.GenerateCount = int.Parse(xAttributeCount.Value);
            }

            // goals
            request.MaxGoals = DefaultMaxGoals;
            var xAttributeGoals = xRequest.Attribute("max-goals");
            if(xAttributeGoals != null)
            {
                request.MaxGoals = int.Parse(xAttributeGoals.Value);
            }

            //league
            request.LeagueName = null;
            var xElementLeague = xRequest.Element("league");
            if(xElementLeague != null)
            {
                request.LeagueName = xElementLeague.Value;
            }

            //StartDate
            request.StartDate = DefaultStartDate;
            var xElementStartDate = xRequest.Element("start-date");
            if (xElementStartDate != null)
            {
                request.StartDate = DateTime.Parse(xElementStartDate.Value);
            }

            // EndDate
            request.EndDate = DefaultEndDate;
            var xElementEndDate = xRequest.Element("end-date");
            if (xElementEndDate != null)
            {
                request.EndDate = DateTime.Parse(xElementEndDate.Value);
            }

            return request;
        }

        private static void ProcessRequest(Request request)
        {
            var context = new FootballEntities();
            var teamsForMatchesQuery = context.Teams.AsQueryable();

            var league = context.Leagues.FirstOrDefault(l => l.LeagueName == request.LeagueName);
            if(request.LeagueName != null)
            {
                teamsForMatchesQuery = teamsForMatchesQuery.Where(
                    t => t.Leagues.Select(l => l.Id).Contains(league.Id));
            }

            var teams = teamsForMatchesQuery.ToList();

            var rnd = new Random();
            var diffDays = (request.EndDate - request.StartDate).Days;
            for (int i = 0; i < request.GenerateCount; i++)
            {
                var match = new TeamMatch();
                match.TeamHome = teams[rnd.Next(teams.Count)];
                match.TeamAway = teams[rnd.Next(teams.Count)];
                match.HomeGoals = rnd.Next(request.MaxGoals + 1);
                match.AwayGoals = rnd.Next(request.MaxGoals + 1);
                match.MatchDate = request.StartDate.AddDays(rnd.Next(diffDays + 1));
                match.League = league;

                context.TeamMatches.Add(match);
                context.SaveChanges();

                Console.WriteLine("{0}: {1} - {2}: {3}-{4} ({5})",
                    match.MatchDate.Value.ToString("dd-MMM-yyyy"),
                    match.TeamHome.TeamName,
                    match.TeamAway.TeamName,
                    match.HomeGoals,
                    match.AwayGoals,
                    match.League != null ? match.League.LeagueName : "no league");
            }
        }
    }
}
