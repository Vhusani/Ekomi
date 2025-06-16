using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Services.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<IEnumerable<OrderDetails>> GetOrderDetails(string OrderId);
        Task<OrderSummary> GetOrderSummary(string OrderId);
        Task<OrderDTO> GetOrder(string OrderId);
        Task<int> UpdateOrderStatus([FromBody] UpdateOrderStatusDTO data);

    }
}
