using System.Text.Json.Serialization;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.Common.Response
{
    public class GetOrderListSuccessModel
    {
        [JsonPropertyName("productCategory")]
        public ProductCategory? Category { get; set; }

        [JsonPropertyName("orderList")]
        public List<OrderModel> OrderList { get; set; }
    }

    public class OrderModel
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        [JsonPropertyName("product")]
        public ProductModel Product { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public double OrderPrice { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
        
        private OrderModel(Order order)
        {
            OrderId = order.OrderId;
            Status = order.Status;
            Product = order.Product;
            Quantity = order.Quantity;
            OrderPrice = order.OrderPrice;
            User = order.User;
    }
        
        public static implicit operator OrderModel(Order o) => new (o);
        public static implicit operator Order(OrderModel om) => new ()
        {
            OrderId = om.OrderId,
            Status = om.Status,
            Product = om.Product,
            Quantity = om.Quantity,
            OrderPrice = om.OrderPrice,
            User = om.User
        };
    }
}
