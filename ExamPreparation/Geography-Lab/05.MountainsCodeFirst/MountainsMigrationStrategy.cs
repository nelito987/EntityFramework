using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.MountainsCodeFirst
{
    public class MountainsMigrationStrategy: 
        DropCreateDatabaseIfModelChanges<MountainsContext>
    {
        protected override void Seed(MountainsContext context)
        {
            var bulgaria = new Country() {Code = "BG", Name = "Bulgaria"};
            context.Countries.Add(bulgaria);
            var germany = new Country() {Code = "DE", Name = "Germany"};
            context.Countries.Add(germany);

            var rila = new Mountain { Name = "Rila", Countries = { bulgaria } };
            context.Mountains.Add(rila);
            var pirin = new Mountain { Name = "Pirin", Countries = { bulgaria } };
            context.Mountains.Add(pirin);
            var rhodopes = new Mountain { Name = "Rhodopes", Countries = { bulgaria } };
            context.Mountains.Add(rhodopes);

            var musala = new Peak { Name = "Musala", Elevation = 2925, Mountain = rila };
            context.Peaks.Add(musala);
            var malyovitsa = new Peak { Name = "Malyovitsa", Elevation = 2729, Mountain = rila };
            context.Peaks.Add(malyovitsa);
            var vihren = new Peak { Name = "Vihren", Elevation = 2914, Mountain = pirin };
            context.Peaks.Add(vihren);
        }
    }
}
