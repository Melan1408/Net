using System.ComponentModel.DataAnnotations;

namespace MovieManager.Contract.Requests
{
    public class UpsertDirectorRequest
    {
        public int DirectorId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
