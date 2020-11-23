using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFerit.DataEntities
{
    [Table("SeoSettings")]
    public class SeoSettings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(250)]
        public string Keywords { get; set; }
        [Required, StringLength(250)]
        public string Description { get; set; }
        [Required, StringLength(250)]
        public string Title { get; set; }
        [Required]
        public string LogoImage { get; set; }
        [Required]
        public string Favicon { get; set; }
     
    }
}
