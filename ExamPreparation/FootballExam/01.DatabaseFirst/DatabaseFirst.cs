using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.DatabaseFirst
{
    class DatabaseFirst
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();

            var teamName = context.Teams.Select(t => t.TeamName);
            
            foreach(var name in teamName)
            {
                Console.WriteLine(name);
            }
        }
    }
}
