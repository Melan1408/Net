using Store.Data.Entites;

namespace Store.Contract.Responses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public string Name { get; set; }

        public ProductResponse Product { get; set; }
    }
}
