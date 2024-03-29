namespace Store.Contract.Responses
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public virtual CategoryResponse Category { get; set; }
    }
}
