using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BlogFerit.DAL.EF
{
    public class Initializer : CreateDatabaseIfNotExists<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            base.Seed(context);
        }
    }
}
