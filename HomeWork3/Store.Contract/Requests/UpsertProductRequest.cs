using System.ComponentModel.DataAnnotations;

namespace Store.Contract.Requests
{
    public class UpsertProductRequest
    {
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
