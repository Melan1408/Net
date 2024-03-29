using Store.Data.Entites;

namespace Store.Contract.Responses
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public virtual OrderResponse Order { get; set; }
    }
}
