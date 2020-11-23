using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogFerit.DAL.EF;

namespace BlogFerit.BL
{
    public class Test
    {
        public Test()
        {
            DataContext db = new DataContext();
            db.Database.Delete();
            db.Database.CreateIfNotExists();
        }
    }
}
