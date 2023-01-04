using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DBModels
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CustomerId { get; set; }
        public string Login { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
