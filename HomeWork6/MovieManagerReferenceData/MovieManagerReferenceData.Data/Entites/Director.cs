using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieManagerReferenceData.Data.Entites
{
    [Table("Director")]
    public class Director
    {
        [Key]
        public int DirectorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        public int Age { get; set; }

    }
}
