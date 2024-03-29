using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Customers;
using Store.Service.Customers.Commands;

namespace Store.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync([FromServices] IRequestHandler<IList<CustomerResponse>> getCustomersQuery)
        {
            return Ok(await getCustomersQuery.Handle());
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int customerId, [FromServices] IRequestHandler<int, CustomerResponse> getCustomerByIdQuery)
        {
            return Ok(await getCustomerByIdQuery.Handle(customerId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCustomerAsync([FromServices] IRequestHandler<UpsertCustomerCommand, CustomerResponse> upsertCustomerCommand, [FromBody] UpsertCustomerRequest request)
        {
            var customer = await upsertCustomerCommand.Handle(new UpsertCustomerCommand
            {
                CustomerId = request.CustomerId,
                FullName = request.FullName,
                Age = request.Age,
                PhoneNumber = request.PhoneNumber,
                OrderId = request.OrderId
            });
            
            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerById(int customerId, [FromServices] IRequestHandler<DeleteCustomerCommand, bool> deleteCustomerByIdCommand)
        {
            var result = await deleteCustomerByIdCommand.Handle(new DeleteCustomerCommand { CustomerId = customerId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
