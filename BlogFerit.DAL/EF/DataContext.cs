using BlogFerit.DataEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFerit.DAL.EF
{
    public class DataContext : DbContext
    {
        public DbSet<Author> authors { get; set; }
        public DbSet<Article> article { get; set; }
        public DbSet<SeoSettings> seoSettings { get; set; }
        public DbSet<Categories> categories { get; set; }
    }
}
