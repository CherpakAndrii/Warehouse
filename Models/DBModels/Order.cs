using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DBModels
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public bool IsSent { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double OrderPrice { get; set; }
        public Customer Customer { get; set; }
    }
}
