using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new GringottsContext();
            var wizardsFirstNames = context.WizzardDeposits
                                             .Where(d => d.DepositGroup == "Troll Chest")
                                             .Select(d => d.FirstName.Substring(0, 1))
                                             .Distinct()
                                             .OrderBy(s => s);
            foreach (var letter in wizardsFirstNames)
            {
                Console.WriteLine(letter);
            }
        }
    }
}
