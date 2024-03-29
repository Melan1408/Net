using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entites;

namespace Store.Service.Customers.Commands
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly StoreContext _context;

        public DeleteCustomerCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
