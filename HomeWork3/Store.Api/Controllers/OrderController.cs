using Microsoft.AspNetCore.Mvc;
using Store.Contract.Requests;
using Store.Contract.Responses;
using Store.Service;
using Store.Service.Customers;
using Store.Service.Customers.Commands;
using Store.Service.Orders.Commands;

namespace Store.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrdersQuery)
        {
            return Ok(await getOrdersQuery.Handle());
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse> getOrderByIdQuery)
        {
            return Ok(await getOrderByIdQuery.Handle(orderId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertOrderAsync([FromServices] IRequestHandler<UpsertOrderCommand, OrderResponse> upsertOrderCommand, [FromBody] UpsertOrderRequest request)
        {
            var order = await upsertOrderCommand.Handle(new UpsertOrderCommand
            {
               OrderId = request.OrderId,
               Name = request.Name,
               ProductId = request.ProductId
            });
            
            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderById(int orderId, [FromServices] IRequestHandler<DeleteOrderCommand, bool> deleteOrderByIdCommand)
        {
            var result = await deleteOrderByIdCommand.Handle(new DeleteOrderCommand { OrderId = orderId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
