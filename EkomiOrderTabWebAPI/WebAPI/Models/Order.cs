namespace WebAPI.Models
{

    public class OrderDTO
    {
        public string? OrderId { get; set; }
        public string? PID { get; set; }
        public string? UserId { get; set; }
        public int OrderStatus { get; set; }
        public decimal OrderTotal { get; set; }
        public long TotalOrderItems { get; set; }
        public DateTime DateCreated { get; set; }

    }

    public class OrderDetails
    {
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        //Product name, image, price would have been copied over from product table, reason for that is if product details change
        //then order details will remail cosistent
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderSummary
    {
        public long TotalItemsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class UpdateOrderStatusDTO
    {
        public string? OrderId { get; set; }
        public int OrderStatusId { get; set; }
    }

}
