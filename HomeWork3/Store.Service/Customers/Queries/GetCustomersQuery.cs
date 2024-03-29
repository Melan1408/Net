using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;
using System;

namespace Store.Service.Customers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<IList<CustomerResponse>>
    {
        private readonly StoreContext _context;

        public GetCustomersQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<CustomerResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(x => x.Order)
                .ThenInclude(p => p.Product)
                .ThenInclude(c => c.Category)
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
                .OrderByDescending(x => x.CustomerId)
                .ToListAsync(cancellationToken);
        }
    }
}
