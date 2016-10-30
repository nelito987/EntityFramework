using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.MountainsCodeFirst
{
    class MountainscodeFirst
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MountainsMigrationStrategy());

            Country c = new Country() { Code = "AB", Name = "Absurdistan" };
            Mountain m = new Mountain { Name = "Absurdistan Mountain" };
            m.Peaks.Add(new Peak() { Name = "Great Peak", Mountain = m });
            m.Peaks.Add(new Peak() { Name = "Small Peak", Mountain = m });
            c.Mountains.Add(m);

            var context = new MountainsContext();
            context.Countries.Add(c);
            context.SaveChanges();
        }
    }
}
