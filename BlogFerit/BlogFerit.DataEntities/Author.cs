using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFerit.DataEntities
{
    [Table("Author")]
    public class Author

    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string NameSurname { get; set; }
        [Required, StringLength(250)]
        public string AuthorAbout { get; set; }
        [Required, StringLength(250)]
        public string ImageUrl { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(50)]
        public string GithubUrl { get; set; }
        [Required, StringLength(50)]
        public string LinkedinUrl { get; set; }
        [Required, StringLength(50)]
        public string FacebookUrl { get; set; }
        [Required, StringLength(50)]
        public string TwitterUrl { get; set; }

    }
}
