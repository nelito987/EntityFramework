using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDefect.Data
{
    class Class1
    {
        static void Main(string[] args)
        {
            var context = new MassDefectDB();
            context.Anomalies.Count();
            using (context)
            {
                context.SaveChanges();
            }
        }
    }
}
