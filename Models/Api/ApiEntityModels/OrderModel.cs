using System.Text.Json.Serialization;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.ApiEntityModels;

public class OrderModel
{
    [JsonPropertyName("orderId")]
    public int OrderId { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("product")]
    public int ProductId { get; set; }
    [JsonPropertyName("quantity")]
    public uint Quantity { get; set; }
    [JsonPropertyName("price")]
    public double OrderPrice { get; set; }
    [JsonPropertyName("user")]
    public int UserId { get; set; }
        
    public OrderModel(Order order)
    {
        OrderId = order.OrderId;
        Status = order.Status.ToString();
        ProductId = order.ProductId;
        Quantity = order.Quantity;
        OrderPrice = order.OrderPrice;
        UserId = order.UserId;
    }
        
    public static implicit operator OrderModel(Order o) => new (o);
    public static implicit operator Order(OrderModel om) => new ()
    {
        OrderId = om.OrderId,
        Status = Enum.Parse<OrderStatus>(om.Status),
        ProductId = om.ProductId,
        Quantity = om.Quantity,
        OrderPrice = om.OrderPrice,
        UserId = om.UserId
    };
}