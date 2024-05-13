using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieManagerReferenceData.Data.Entites
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

    }
}
