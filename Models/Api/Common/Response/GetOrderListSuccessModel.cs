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
        
        public OrderModel(Order order)
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
    
    public class UserModel
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string EncryptedPassword { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("role")]
        public UserRole Role { get; set; }
        
        private UserModel(User user)
        {
            UserId = user.UserId.Value;
            Login = user.Login;
            EncryptedPassword = user.EncryptedPassword;
            Name = user.Name;
            Email = user.Email;
            Phone = user.Phone;
            Role = user.Role;
        }
        
        public static implicit operator UserModel(User u) => new (u);
        public static implicit operator User(UserModel um) => new ()
        {
            UserId = um.UserId,
            Login = um.Login,
            EncryptedPassword = um.EncryptedPassword,
            Name = um.Name,
            Email = um.Email,
            Phone = um.Phone,
            Role = um.Role
        };
    }
}
