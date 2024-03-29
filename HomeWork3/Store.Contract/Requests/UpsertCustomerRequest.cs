using System.ComponentModel.DataAnnotations;

namespace Store.Contract.Requests
{
    public class UpsertCustomerRequest
    {
        public int CustomerId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int OrderId { get; set; }

    }
}
