using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace Store.Service.Customers.Commands
{
    public class UpsertCustomerCommand
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public int OrderId { get; set; }

        public Customer UpsertCustomer()
        {
            var customer = new Customer
            {
                CustomerId = CustomerId,
                FullName = FullName,
                Age = Age,
                PhoneNumber = PhoneNumber,
                OrderId = OrderId
            };

            return customer;
        }
    }

    public class UpsertCustomerCommandHandler : IRequestHandler<UpsertCustomerCommand, CustomerResponse>
    {
        private readonly StoreContext _context;

        public UpsertCustomerCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(UpsertCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                customer = request.UpsertCustomer();
                await _context.AddAsync(customer, cancellationToken);
            }
            else
            {
                customer.CustomerId = request.CustomerId;
                customer.FullName = request.FullName;
                customer.Age = request.Age;
                customer.PhoneNumber = request.PhoneNumber;
                customer.OrderId = request.OrderId;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                FullName = request.FullName,
                Age = request.Age,
                PhoneNumber = request.PhoneNumber,
                Order = customer?.Order != null ? new OrderResponse
                {
                    OrderId = customer.Order.OrderId,
                    Name = customer.Order.Name,
                    Product = customer?.Order.Product != null ? new ProductResponse
                    {
                        ProductId = customer.Order.Product.ProductId,
                        CategoryId = customer.Order.Product.CategoryId,
                        Name = customer.Order.Product.Name,
                        Category = customer?.Order.Product.Category != null ? new CategoryResponse
                        {
                            CategoryId = customer.Order.Product.Category.CategoryId,
                            Name = customer.Order.Product.Category.Name
                        } : null
                    } : null
                } : null
            };
        }

        private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.Include(x => x.Order)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
