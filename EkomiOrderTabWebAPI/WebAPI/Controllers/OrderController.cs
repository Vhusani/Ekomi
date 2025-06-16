using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.Order;

namespace WebAPI.Controllers
{
    [Route("order/")]
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService orderservice { get; }

        public OrderController(IOrderService _orderservice) 
        { 
            orderservice = _orderservice;
        }

        [HttpGet]
        [Route("get/all")]
        public async Task<ActionResult<OrderDTO>> GetOrders()
        {
            try
            {
                var Orders = await orderservice.GetOrders();

                if (!(Orders is null))
                {
                    return Ok(Orders);
                }
                else
                {
                    return BadRequest("We couldnt process your request at the moment, Please contact support");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("get/details/{OrderId}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetails(string OrderId)
        {
            try
            {
                var result = await orderservice.GetOrderDetails(OrderId);

                if (!(result is null))
                {
                    return Ok(result);
                }
                else {
                    return BadRequest("Failed to get order details");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("get/summary/{OrderId}")]
        public async Task<ActionResult<OrderDetails>> GetOrderSummary(string OrderId)
        {
            try
            {
                var result = await orderservice.GetOrderSummary(OrderId);

                if (!(result is null))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("get/{OrderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(string OrderId)
        {
            try
            {
                var result = await orderservice.GetOrder(OrderId);

                if (!(result is null))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/status")]
        public async Task<ActionResult<int>> UpdateOrderStatus([FromBody] UpdateOrderStatusDTO Data)
        {
            try
            {
                var updatedRows = await orderservice.UpdateOrderStatus(Data);

                if (updatedRows > 0)
                {
                    return Ok(updatedRows);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
