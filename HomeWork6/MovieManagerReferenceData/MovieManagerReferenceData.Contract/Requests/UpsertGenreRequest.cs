using System.ComponentModel.DataAnnotations;

namespace MovieManagerReferenceData.Contract.Requests
{
    public class UpsertGenreRequest
    {
        public int GenreId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
