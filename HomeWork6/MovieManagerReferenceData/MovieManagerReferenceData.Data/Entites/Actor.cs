using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieManagerReferenceData.Data.Entites
{
    [Table("Actor")]
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }

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
