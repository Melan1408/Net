using System.ComponentModel.DataAnnotations;

namespace Store.Contract.Requests
{
    public class UpsertCategoryRequest
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
