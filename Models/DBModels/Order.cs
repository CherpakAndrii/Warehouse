using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DBModels.Enums;

namespace Models.DBModels
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double OrderPrice { get; set; }
        public User User { get; set; }
    }
}
