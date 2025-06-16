using Dapper;
using System.Data;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services.Order
{
    public class OrderService: IOrderService
    {
        public IDatabaseConnectionFactory ConnectionFactory { get; }
        public OrderService(IDatabaseConnectionFactory connection) 
        {  
            ConnectionFactory = connection;
        }
        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            IDbConnection connection = ConnectionFactory.GetThemLive();

            using (connection)
            {
                var sqlQuery = "SELECT * FROM `Order`";
                var result = await connection.QueryAsync<OrderDTO>(sqlQuery,
                    commandType: CommandType.Text,
                    commandTimeout: int.MaxValue);

                var ordersList = new List<OrderDTO>();

                if (result != null)
                {
                    foreach (var order in result)
                    {
                        var orderSummary = await GetOrderSummary(order.OrderId);

                        var dto = new OrderDTO
                        {
                            OrderId = order.OrderId,
                            PID = order.PID,
                            UserId = order.UserId,
                            OrderStatus = order.OrderStatus,
                            OrderTotal = orderSummary.TotalRevenue,
                            TotalOrderItems = orderSummary.TotalItemsSold,
                            DateCreated = order.DateCreated
                        };

                        ordersList.Add(dto);
                    }
                }

                return ordersList;
            }
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetails(string OrderId)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("OrderId", OrderId);
            IDbConnection connection = ConnectionFactory.GetThemLive();

            using (connection)
            {
                var SqlQuery = "SELECT * FROM OrderDetails WHERE OrderId = @OrderId";
                var result = await connection.QueryAsync<OrderDetails>(SqlQuery, dp,
                    commandType: CommandType.Text,
                    commandTimeout: int.MaxValue);
                return result;  
            }
        }

        public async Task<OrderSummary> GetOrderSummary(string OrderId)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("OrderId", OrderId);
            IDbConnection connection = ConnectionFactory.GetThemLive();

            using (connection) 
            {
                var SqlQuery = "SELECT * FROM OrderDetails WHERE OrderId = @OrderId";
                var Items = await connection.QueryAsync<OrderDetails>(SqlQuery, dp,
                    commandType: CommandType.Text,
                    commandTimeout: int.MaxValue);

                if (Items != null)
                {
                    var dto = new OrderSummary
                    {
                        TotalItemsSold = Items.Sum(e => e.Quantity),
                        TotalRevenue = Math.Round(Items.Sum(e => e.Quantity * e.Price), 2)
                    };
                    return dto;
                }
            }            
            return null;
        }

       public async Task<OrderDTO> GetOrder(string OrderId)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("OrderId", OrderId);
            IDbConnection connection = ConnectionFactory.GetThemLive();

            using (connection)
            {
                var sqlQuery = "SELECT * FROM `Order` WHERE OrderId = @OrderId";
                var result = await connection.QueryFirstOrDefaultAsync<OrderDTO>(
                    sqlQuery, dp,
                    commandType: CommandType.Text,
                    commandTimeout: int.MaxValue
                );
                return result;
            }
        }

        public async Task<int> UpdateOrderStatus(UpdateOrderStatusDTO data)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("OrderId", data.OrderId);
            dp.Add("StatusId", data.OrderStatusId);

            IDbConnection connection = ConnectionFactory.GetThemLive();

            using (connection)
            {
                var sqlQuery = "UPDATE `Order` SET OrderStatus = @StatusId WHERE OrderId = @OrderId";
                var updatedRows = await connection.ExecuteAsync(
                    sqlQuery, dp,
                    commandType: CommandType.Text,
                    commandTimeout: int.MaxValue
                );
                return updatedRows;
            }
        }

    }
}
