using System.ComponentModel.DataAnnotations;

namespace MovieManagerReferenceData.Contract.Requests
{
    public class UpsertActorRequest
    {
        public int ActorId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
