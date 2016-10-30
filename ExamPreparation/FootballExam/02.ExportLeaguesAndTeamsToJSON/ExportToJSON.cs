namespace _02.ExportLeaguesAndTeamsToJSON
{
    using _01.DatabaseFirst;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;

    class ExportLeaguesAndTeamsAsJson
    {
        static void Main()
        {
            var context = new FootballEntities();
            var leaguesWithTeams = context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
                {
                    leagueName = l.LeagueName,
                    teams = l.Teams
                        .OrderBy(t => t.TeamName)
                        .Select(t => t.TeamName)
                })
                .ToList();


            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(leaguesWithTeams);
            File.WriteAllText("leagues-and-teams.json", json);
            Console.WriteLine("File leagues-and-teams.json exported.");
        }
    }
}
