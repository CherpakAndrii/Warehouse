using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DBModels.Enums;

namespace Models.DBModels
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ProductId { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public uint Quantity { get; set; }
        public int AvailableAmount { get; set; }
        public ProductCategory Category { get; set; }
    }
}