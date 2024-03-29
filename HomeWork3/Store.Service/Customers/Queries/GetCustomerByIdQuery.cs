using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Customers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<int, CustomerResponse>
    {
        private readonly StoreContext _context;

        public GetCustomerByIdQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()              
                .Include(x => x.Order)
                .ThenInclude(p => p.Product)
                .ThenInclude(c => c.Category)
                .Where(x => x.CustomerId == customerId)
                .Select(x => new CustomerResponse
                {
                    CustomerId = x.CustomerId,
                    FullName = x.FullName,
                    Age = x.Age,
                    PhoneNumber = x.PhoneNumber,
                    Order = new OrderResponse
                    {
                        OrderId = x.Order.OrderId,
                        Name = x.Order.Name,
                        Product = new ProductResponse
                        {
                            ProductId = x.Order.Product.ProductId,
                            CategoryId = x.Order.Product.CategoryId,
                            Name = x.Order.Product.Name,
                            Price = x.Order.Product.Price,
                            Category = new CategoryResponse
                            {
                                CategoryId = x.Order.Product.Category.CategoryId,
                                Name = x.Order.Product.Category.Name
                            }
                        }
                    }
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
