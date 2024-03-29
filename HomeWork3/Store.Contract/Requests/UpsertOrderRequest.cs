using System.ComponentModel.DataAnnotations;

namespace Store.Contract.Requests
{
    public class UpsertOrderRequest
    {
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ProductId { get; set; }

    }
}
